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
    public class WorkoutController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Workout
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new WorkoutService(userID);
            var model = service.GetWorkouts();

            return View(model);
        }

        // GET : Create
        public ActionResult Create()
        {
            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID","Shoe");

            return View();
        }

        // POST : Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkoutCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateWorkoutService();

            if (service.CreateWorkout(model))
            {
                TempData["SaveResult"] = "Your workout was created.";
                return RedirectToAction("Index");
            };

            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Shoe");

            return View(model);
        }

        // GET : Details
        public ActionResult Details(int id)
        {
            var service = CreateWorkoutService();
            var model = service.GetWorkoutByID(id);

            return View(model);
        }

        // GET : Edit
        public ActionResult Edit(int id)
        {
            var service = CreateWorkoutService();
            var detail = service.GetWorkoutByID(id);
            var model =
                new WorkoutEdit
                {
                    ID = detail.ID,
                    Date = detail.Date,
                    Distance = detail.Distance,
                    CompletionTime = detail.CompletionTime,
                    Comments = detail.Comments
                };

            return View(model);
        }

        // POST : Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, WorkoutEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateWorkoutService();

            if (service.UpdateWorkout(model))
            {
                TempData["SaveResult"] = "Your workout was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your workout could not be updated.");
            return View(model);
        }

        // GET : Delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateWorkoutService();
            var model = service.GetWorkoutByID(id);

            return View(model);
        }

        // POST : Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateWorkoutService();

            service.DeleteWorkout(id);

            TempData["SaveResult"] = "Your workout was deleted.";

            return RedirectToAction("Index");
        }


        private WorkoutService CreateWorkoutService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new WorkoutService(userID);
            return service;
        }
    }
}