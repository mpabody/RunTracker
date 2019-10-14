using BlueBadgeRunTracker.Data;
using Microsoft.AspNet.Identity;
using PagedList;
using RunTracker.Models.RaceModels;
using RunTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueBadgeRunTracker.Controllers
{
    public class AllRacesModel
    {
        public IPagedList<RaceListItem> InterestedRaces { get; set; }

        public IPagedList<RaceListItem> RanRaces { get; set; }
    }

    [Authorize]
    public class RaceController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        //--------------------------------------
        [ActionName("AllRaces")]
        public PartialViewResult RacePartials()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RaceService(userID);
            var model = service.GetAllRaces();

            return PartialView(model);
        }
        //--------------------------------------

        // Index All races
        [ActionName("IndexAll")]
        public ActionResult IndexAll(string sortOrderI, string sortOrderR, string searchStringI, string searchStringR, string currentFilterI, string currentFilterR, int? pageI, int? pageR)
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RaceService(userID);
            var races = service.GetAllRaces();
            var modelI = races.Where(r => r.CompletionTime == null);
            var modelR = races.Where(r => r.CompletionTime != null);

            ViewBag.CurrentSortI = sortOrderI;
            ViewBag.DateSortI = String.IsNullOrEmpty(sortOrderI) ? "date_desc" : "";
            ViewBag.NameSortI = sortOrderI == "Name" ? "name_desc" : "Name";
            ViewBag.LocationSortI = sortOrderI == "Location" ? "location_desc" : "Location";
            ViewBag.DistanceSortI = sortOrderI == "Distance" ? "dist_desc" : "Distance";


            if (searchStringI != null)
            {
                pageI = 1;
            }
            else
            {
                searchStringI = currentFilterI;
            }

            ViewBag.CurrentFilterI = searchStringI;

            if (!String.IsNullOrEmpty(searchStringI))
            {
                modelI = modelI.Where(m => m.Name.ToLower().Contains(searchStringI)
                                || m.Distance.ToString().Contains(searchStringI)
                                || m.Location.ToLower().Contains(searchStringI)
                                || m.Date.ToString().Contains(searchStringI));
            }

            switch (sortOrderI)
            {
                case "date_desc":
                    modelI = modelI.OrderByDescending(s => s.Date);
                    break;
                case "Name":
                    modelI = modelI.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    modelI = modelI.OrderByDescending(s => s.Name);
                    break;
                case "Distance":
                    modelI = modelI.OrderBy(s => s.Distance);
                    break;
                case "dist_desc":
                    modelI = modelI.OrderByDescending(s => s.Distance);
                    break;
                case "Location":
                    modelI = modelI.OrderBy(s => s.Location);
                    break;
                case "location_desc":
                    modelI = modelI.OrderByDescending(s => s.Location);
                    break;
                default: // Date Decending
                    modelI = modelI.OrderBy(s => s.Date);
                    break;
            }



            ViewBag.CurrentSortR = sortOrderR;
            ViewBag.DateSortR = String.IsNullOrEmpty(sortOrderR) ? "date_desc" : "";
            ViewBag.NameSortR = sortOrderR == "Name" ? "name_desc" : "Name";
            ViewBag.LocationSortR = sortOrderR == "Location" ? "location_desc" : "Location";
            ViewBag.DistanceSortR = sortOrderR == "Distance" ? "dist_desc" : "Distance";

            if (searchStringR != null)
            {
                pageR = 1;
            }
            else
            {
                searchStringR = currentFilterR;
            }

            ViewBag.CurrentFilterR = searchStringR;

            if (!String.IsNullOrEmpty(searchStringR))
            {
                modelR = modelR.Where(m => m.Name.ToLower().Contains(searchStringR)
                                || m.Distance.ToString().Contains(searchStringR)
                                || m.Location.Contains(searchStringR)
                                || m.Date.ToString().Contains(searchStringR));
            }

            switch (sortOrderR)
            {
                case "date_desc":
                    modelR = modelR.OrderByDescending(s => s.Date);
                    break;
                case "Name":
                    modelR = modelR.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    modelR = modelR.OrderByDescending(s => s.Name);
                    break;
                case "Distance":
                    modelR = modelR.OrderBy(s => s.Distance);
                    break;
                case "dist_desc":
                    modelR = modelR.OrderByDescending(s => s.Distance);
                    break;
                case "Location":
                    modelR = modelR.OrderBy(s => s.Location);
                    break;
                case "location_desc":
                    modelR = modelR.OrderByDescending(s => s.Location);
                    break;
                default: // Date Decending
                    modelR = modelR.OrderBy(s => s.Date);
                    break;
            }

            int pageSizeI = 10;
            int pageNumberI = (pageI ?? 1);
            int pageSizeR = 10;
            int pageNumberR = (pageR ?? 1);

            var newModel = new AllRacesModel
            {
                InterestedRaces = modelI.ToPagedList(pageNumberI, pageSizeI),
                RanRaces = modelR.ToPagedList(pageNumberR, pageSizeR)
            };

            return View(newModel);
        }

        // GET: Races Interested
        [ActionName("IndexInterested")]
        public ActionResult IndexInterested(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RaceService(userID);
            var model = service.GetRacesInterested();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.Name = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.LocationSort = sortOrder == "Location" ? "location_desc" : "Location";
            ViewBag.DistSort = sortOrder == "Distance" ? "dist_desc" : "Distance";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(m => m.Name.ToLower().Contains(searchString)
                                || m.Distance.ToString().Contains(searchString)
                                || m.Location.Contains(searchString)
                                || m.Date.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date_desc":
                    model = model.OrderByDescending(s => s.Date);
                    break;
                case "Name":
                    model = model.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    model = model.OrderByDescending(s => s.Name);
                    break;
                case "Distance":
                    model = model.OrderBy(s => s.Distance);
                    break;
                case "dist_desc":
                    model = model.OrderByDescending(s => s.Distance);
                    break;
                case "Location":
                    model = model.OrderBy(s => s.Location);
                    break;
                case "location_desc":
                    model = model.OrderByDescending(s => s.Location);
                    break;
                default: // Date Decending
                    model = model.OrderBy(s => s.Date);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
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
        public ActionResult CreateInterested(RaceInterestedCreate model)
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
        public ActionResult DeleteInterested(int id)
        {
            var svc = CreateRaceService();
            var model = svc.GetRaceInterestedByID(id);

            return View(model);
        }

        // POST : Delete Interested
        [HttpPost]
        [ActionName("DeleteInterested")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostInterested(int id)
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
        public ActionResult IndexRan(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RaceService(userID);
            var model = service.GetRacesRan();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSort = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSort = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.LocationSort = sortOrder == "Location" ? "location_desc" : "Location";
            ViewBag.DistSort = sortOrder == "Distance" ? "dist_desc" : "Distance";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(m => m.Name.ToLower().Contains(searchString)
                                || m.Distance.ToString().Contains(searchString)
                                || m.Location.ToLower().Contains(searchString)
                                || m.Date.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Date":
                    model = model.OrderBy(s => s.Date);
                    break;
                case "Name":
                    model = model.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    model = model.OrderByDescending(s => s.Name);
                    break;
                case "Distance":
                    model = model.OrderBy(s => s.Distance);
                    break;
                case "dist_desc":
                    model = model.OrderByDescending(s => s.Distance);
                    break;
                case "Location":
                    model = model.OrderBy(s => s.Location);
                    break;
                case "location_desc":
                    model = model.OrderByDescending(s => s.Location);
                    break;
                default: // Date Decending
                    model = model.OrderByDescending(s => s.Date);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }


        // GET : Create Ran
        [ActionName("CreateRan")]
        public ActionResult CreateRan()
        {
            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name");
            return View();
        }

        // POST : Create Ran
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRan(RaceRanCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name", model.ShoeID);
                return View(model);
            }

            var service = CreateRaceService();

            if (service.CreateRaceRan(model))
            {
                TempData["SaveResult"] = "Your race was created.";
                return RedirectToAction("IndexRan");
            };

            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name", model.ShoeID);

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
                    ShoeID = detail.ShoeID,
                    Shoe = detail.Shoe
                };

            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name", model.ShoeID);

            return View(model);
        }

        // POST : Edit Ran
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRan(int id, RaceRanEdit model)
        {
            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name", model.ShoeID);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

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

            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name", model.ShoeID);
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

        //---------------------------------------

        // Convert RaceInterested to RaceRan

        // GET : Convert
        [ActionName("ConvertToRan")]
        public ActionResult ConvertToRan(int id)
        {
            var service = CreateRaceService();
            var detail = service.GetRaceToConvertByID(id);
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
                    ShoeID = detail.ShoeID,
                    Shoe = detail.Shoe
                };

            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name", model.ShoeID);

            return View(model);
        }

        //POST : ConvertToRan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConvertToRan(int id, RaceRanEdit model)
        {
            ViewBag.ShoeID = new SelectList(_db.Shoes.ToList(), "ShoeID", "Name", model.ShoeID);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.RaceID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateRaceService();

            if (service.ConvertFromInterestedToRan(model))
            {
                TempData["SaveResult"] = "Your race was updated.";
                return RedirectToAction("IndexRan");
            }

            ModelState.AddModelError("", "Your race could not be updated.");
            return View(model);
        }

        private RaceService CreateRaceService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RaceService(userID);
            return service;
        }
    }
}