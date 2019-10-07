using BlueBadgeRunTracker.Data;
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
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Shoe
        public ActionResult Index(string sortOrder, string searchString)
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ShoeService(userID);
            var model = service.GetShoes();


            ViewBag.NameSortAlph = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.MilesRunSort = sortOrder == "Mileage" ? "mileage_desc" : "Mileage";

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(m => m.Name.ToLower().Contains(searchString)
                                || m.MilesRun.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(s => s.Name);
                    break;
                case "Mileage":
                    model = model.OrderBy(s => s.MilesRun);
                    break;
                case "mileage_desc":
                    model = model.OrderByDescending(s => s.MilesRun);
                    break;
                default:
                    model = model.OrderBy(s => s.Name);
                    break;
            }

            return View(model);
        }

        // GET : Create
        public ActionResult Create()
        {
            ViewBag.returnUrl = Request.UrlReferrer;
            return View();
        }

        // POST : Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShoeCreate model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateShoeService();

            if (service.CreateShoe(model))
            {
                TempData["SaveResult"] = "Your shoe was created.";
                return Redirect(returnUrl);
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
                    Name = detail.Name,
                    Description = detail.Description
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