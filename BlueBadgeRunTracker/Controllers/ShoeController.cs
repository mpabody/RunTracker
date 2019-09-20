using Microsoft.AspNet.Identity;
using RunTracker.Models;
using RunTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueBadgeRunTracker.Controllers
{
    [Authorize]
    public class ShoeController : Controller
    {
        // GET: Shoe
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ShoeService(userID);
            var model = service.GetShoes();

            return View(model);
        }

        // GET : Create
        public ActionResult Create()
        {
            return View();
        }

        // POST : Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShoeCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateShoeService();

            if (service.CreateShoe(model))
            {
                TempData["SaveResult"] = "Your shoe was created.";
                return RedirectToAction("Index");
            };

            return View(model);
        }

        // GET : Details
        public ActionResult Details(int id)
        {
            var svc = CreateShoeService();
            var model = svc.GetShoeByID(id);

            return View(model);
        }

        // GET : Edit
        public ActionResult Edit(int id)
        {
            var service = CreateShoeService();
            var detail = service.GetShoeByID(id);
            var model =
                new ShoeEdit
                {
                    ShoeID = detail.ShoeID,
                    Brand = detail.Brand,
                    Name = detail.Name
                };

            return View(model);
        }

        // POST : Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ShoeEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ShoeID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateShoeService();

            if (service.UpdateShoe(model))
            {
                TempData["SaveResult"] = "Your shoe was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your shoe could not be updated.");
            return View(model);
        }

        //GET : Delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateShoeService();
            var model = svc.GetShoeByID(id);

            return View(model);
        }

        // POST : Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateShoeService();

            service.DeleteShoe(id);

            TempData["SaveResult"] = "Your shoe was deleted.";

            return RedirectToAction("Index");
        }


        private ShoeService CreateShoeService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ShoeService(userID);
            return service;
        }
    }
}