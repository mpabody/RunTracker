using BlueBadgeRunTracker.Data;
using Microsoft.AspNet.Identity;
using RunTracker.Models.RaceModels;
using RunTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueBadgeRunTracker.Controllers
{
    [Authorize]
    public class RaceController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Race
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RaceService(userID);
            var model = service.GetRacesInterested();

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
        public ActionResult Create(RaceInterestedCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateRaceService();

            if (service.CreateRaceInterested(model))
            {
                TempData["SaveResult"] = "Your race was created.";
                return RedirectToAction("Index");
            };

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateRaceService();
            var model = svc.GetRaceByID(id);

            return View(model);
        }

        // GET : Edit
        public ActionResult Edit(int id)
        {
            var service = CreateRaceService();
            var detail = service.GetRaceByID(id);
            var model =
                new RaceInterestedEdit
                {
                    RaceID = detail.RaceID,
                    Date = detail.Date,
                    Name = detail.Name,
                    Location = detail.Location,
                    Distance = detail.Distance,
                    Description = detail.Description,
                    Comments = detail.Comments
                };

            return View(model);
        }

        // POST : Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RaceInterestedEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.RaceID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateRaceService();

            if (service.UpdateRaceInterested(model))
            {
                TempData["SaveResult"] = "Your race was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your race could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateRaceService();
            var model = svc.GetRaceByID(id);

            return View(model);
        }

        // POST : Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateRaceService();

            service.DeleteRace(id);

            TempData["SaveResult"] = "Your shoe was deleted.";

            return RedirectToAction("Index");
        }

        private RaceService CreateRaceService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RaceService(userID);
            return service;
        }
    }
}