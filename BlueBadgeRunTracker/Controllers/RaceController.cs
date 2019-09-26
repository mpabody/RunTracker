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
        [ActionName("IndexInterested")]
        public ActionResult IndexInterested()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RaceService(userID);
            var model = service.GetRacesInterested();

            return View(model);
        }

        // GET : Create
        [ActionName("CreateInterested")]
        public ActionResult CreateInterested()
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
                return RedirectToAction("IndexInterested");
            };

            return View(model);
        }

        // GET : Details Interested
        [ActionName("DetailsInterested")]
        public ActionResult DetailsInterested(int id)
        {
            var svc = CreateRaceService();
            var model = svc.GetRaceInterestedByID(id);

            return View(model);
        }

        // GET : Edit Interested
        [ActionName("EditInterested")]
        public ActionResult EditInterested(int id)
        {
            var service = CreateRaceService();
            var detail = service.GetRaceInterestedByID(id);
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

        // POST : Edit Interested
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInterested(int id, RaceInterestedEdit model)
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
                return RedirectToAction("IndexInterested");
            }

            ModelState.AddModelError("", "Your race could not be updated.");
            return View(model);
        }

        // GET : Delete Interested
        [ActionName("DeleteInterested")]
        public ActionResult Delete(int id)
        {
            var svc = CreateRaceService();
            var model = svc.GetRaceInterestedByID(id);

            return View(model);
        }

        // POST : Delete Interested
        [HttpPost]
        [ActionName("DeleteInterested")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateRaceService();

            service.DeleteRace(id);

            TempData["SaveResult"] = "Your shoe was deleted.";

            return RedirectToAction("IndexInterested");
        }


        // Interested
        //-----------------------------------------------------------
        // Ran


        // GET: Race
        [ActionName("IndexRan")]
        public ActionResult IndexRan()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RaceService(userID);
            var model = service.GetRacesRan();

            return View(model);
        }

        // GET : Edit Ran
        [ActionName("EditRan")]
        public ActionResult EditRan(int id)
        {
            var service = CreateRaceService();
            var detail = service.GetRaceRanByID(id);
            var model =
                new RaceRanEdit
                {
                    RaceID = detail.RaceID,
                    Date = detail.Date,
                    Name = detail.Name,
                    Location = detail.Location,
                    Distance = detail.Distance,
                    Description = detail.Description,
                    Comments = detail.Comments,
                    CompletionTime = detail.CompletionTime,
                    ShoeID = detail.ShoeID
                };
            return View(model);
        }

        // POST : Edit Ran
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRan(int id, RaceRanEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.RaceID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateRaceService();

            if (service.UpdateRaceRan(model))
            {
                TempData["SaveResult"] = "Your race was updated.";
                return RedirectToAction("IndexRan");
            }

            ModelState.AddModelError("", "Your race could not be updated.");
            return View(model);
        }

        // GET : Details Ran
        public ActionResult DetailsRan(int id)
        {
            var svc = CreateRaceService();
            var model = svc.GetRaceRanByID(id);

            return View(model);
        }

        // GET : Delete Ran
        [ActionName("DeleteRan")]
        public ActionResult DeleteRan(int id)
        {
            var svc = CreateRaceService();
            var model = svc.GetRaceRanByID(id);

            return View(model);
        }

        // POST : Delete Ran
        [HttpPost]
        [ActionName("DeleteRan")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostRan(int id)
        {
            var service = CreateRaceService();

            service.DeleteRace(id);

            TempData["SaveResult"] = "Your shoe was deleted.";

            return RedirectToAction("IndexRan");
        }


        private RaceService CreateRaceService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RaceService(userID);
            return service;
        }
    }
}