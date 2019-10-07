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
    public class WorkoutController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Workout
        public ActionResult Index(string sortOrder, string searchString)
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new WorkoutService(userID);
            var model = service.GetWorkouts();

            ViewBag.DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.DistSort = sortOrder == "Distance" ? "dist_desc" : "Distance";
            ViewBag.ShoeSort = sortOrder == "Shoe" ? "shoe_desc" : "Shoe";

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(m => m.Date.ToString().Contains(searchString)
                                || m.Distance.ToString().Contains(searchString)
                                || m.Shoe.Name.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date_desc":
                    model = model.OrderBy(s => s.Date);
                    break;
                case "Distance":
                    model = model.OrderBy(s => s.Distance); // .ThenBy
                    break;
                case "distance_desc":
                    model = model.OrderByDescending(s => s.Distance);
                    break;
                case "Shoe":
                    model = model.OrderBy(s => s.Shoe.Name);
                    break;
                case "shoe_desc":
                    model = model.OrderByDescending(s => s.Shoe.Name);
                    break;
                default:
                    model = model.OrderByDescending(s => s.Date);
                    break;
            }

            return View(model);
        }

        // GET : Create
        public ActionResult Create()
        {
            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name");

            return View();
        }

        // POST : Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkoutCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name");
                return View(model);
            }

            var service = CreateWorkoutService();

            if (service.CreateWorkout(model))
            {
                TempData["SaveResult"] = "Your workout was created.";
                return RedirectToAction("Index");
            };

            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name");

            return View(model);
        }

        // GET : Details
        public ActionResult Details(int id)
        {
            var service = CreateWorkoutService();
            var model = service.GetWorkoutByID(id);

            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name", model.ShoeID);
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
                    WorkoutID = detail.WorkoutID,
                    Date = detail.Date,
                    Distance = detail.Distance,
                    CompletionTime = detail.CompletionTime,
                    Comments = detail.Comments,
                    ShoeID = detail.ShoeID,
                    Shoe = detail.Shoe
                };

            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name", model.ShoeID);

            return View(model);
        }

        // POST : Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, WorkoutEdit model)
        {
            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name", model.ShoeID);

            if (!ModelState.IsValid) return View(model);

            if (model.WorkoutID != id)
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