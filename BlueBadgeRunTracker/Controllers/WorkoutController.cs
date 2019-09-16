using Microsoft.AspNet.Identity;
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
        // GET: Workout
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new WorkoutService(userID);
            var model = service.GetWorkouts();

            return View(model);
        }





        private WorkoutService CreateWorkoutService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new WorkoutService(userID);
            return service;
        }
    }
}