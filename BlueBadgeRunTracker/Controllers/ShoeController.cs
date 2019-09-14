using RunTracker.Models;
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
            var model = new ShoeListItem[0];
            return View(model);
        }
    }
}