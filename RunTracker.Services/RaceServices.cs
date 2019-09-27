using BlueBadgeRunTracker.Data;
using RunTracker.Data;
using RunTracker.Models.RaceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Services
{
    public class RaceService
    {
        private readonly Guid _userID;

        public RaceService(Guid userID)
        {
            _userID = userID;
        }

        // Display list of Races I'm interested in
        public IEnumerable<RaceListItem> GetRacesInterested()
        {
            using (var _db = new ApplicationDbContext())
            {
                var query = _db.Races
                    .Where(r => r.UserID == _userID)
                    .Select(
                        r =>
                            new RaceListItem
                            {
                                RaceID = r.RaceID,
                                Name = r.Name,
                                Date = r.Date,
                                Location = r.Location,
                                Distance = r.Distance
                            }
                    );
                return query.ToList();
            }
        }

        // Display details for a race I'm interested in
        public RaceInterestedDetail GetRaceInterestedByID(int id)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                    .Races
                    .Single(r => r.RaceID == id && r.UserID == _userID && r.CompletionTime == null);
                return
                    new RaceInterestedDetail
                    {
                        RaceID = entity.RaceID,
                        Date = entity.Date,
                        Name = entity.Name,
                        Location = entity.Location,
                        Distance = entity.Distance,
                        Description = entity.Description,
                        Comments = entity.Comments
                    };
            }
        }

        // Created a race I'm interested in
        public bool CreateRaceInterested(RaceInterestedCreate model)
        {
            var entity =
                new Race()
                {
                    UserID = _userID,
                    Date = model.Date,
                    Name = model.Name,
                    Location = model.Location,
                    Distance = model.Distance,
                    Description = model.Description,
                    Comments = model.Comments
                };
            using (var _db = new ApplicationDbContext())
            {
                _db.Races.Add(entity);
                return _db.SaveChanges() == 1;
            }
        }

        // Update a race I'm interested in -- no option to say I ran it
        public bool UpdateRaceInterested(RaceInterestedEdit model)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                    .Races
                    .Single(r => r.RaceID == model.RaceID && r.UserID == _userID && r.CompletionTime == null);

                entity.Date = model.Date;
                entity.Name = model.Name;
                entity.Location = model.Location;
                entity.Distance = model.Distance;
                entity.Description = model.Description;
                entity.Comments = model.Comments;

                return _db.SaveChanges() == 1;
            }
        }


            // Interested
        //---------------------------------------------------------------------
            // Ran


        // Displays list of Races I've run -- completion time NOT equal to null
        public IEnumerable<RaceListItem> GetRacesRan()
        {
            using (var _db = new ApplicationDbContext())
            {
                var query = _db.Races
                    .Where(r => r.UserID == _userID && r.CompletionTime != null)
                    .Select(
                        r =>
                            new RaceListItem
                            {
                                RaceID = r.RaceID,
                                Name = r.Name,
                                Date = r.Date,
                                Location = r.Location,
                                Distance = r.Distance
                            }
                    );
                return query.ToList();
            }
        }
        
        // Display all properties for a race I've run
        public RaceRanDetail GetRaceRanByID(int id)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                    .Races
                    .Single(r => r.RaceID == id && r.UserID == _userID && r.CompletionTime != null);
                return
                    new RaceRanDetail
                    {
                        RaceID = entity.RaceID,
                        Date = entity.Date,
                        Name = entity.Name,
                        Location = entity.Location,
                        Distance = entity.Distance,
                        Description = entity.Description,
                        Comments = entity.Comments,
                        CompletionTime = entity.CompletionTime,
                        ShoeID = entity.ShoeID
                    };
            }
        }

        // Create a race I've run
        public bool CreateRaceRan(RaceRanCreate model)
        {
            var entity =
                new Race()
                {
                    UserID = _userID,
                    Date = model.Date,
                    Name = model.Name,
                    Location = model.Location,
                    Distance = model.Distance,
                    Description = model.Description,
                    Comments = model.Comments,
                    CompletionTime = model.CompletionTime,
                    ShoeID = model.ShoeID
                };
            using (var _db = new ApplicationDbContext())
            {
                _db.Races.Add(entity);
                return _db.SaveChanges() == 1;
            }
        }

        // Update a race I've run
        public bool UpdateRaceRan(RaceRanEdit model)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                    .Races
                    .Single(r => r.RaceID == model.RaceID && r.UserID == _userID && r.CompletionTime != null);

                entity.Date = model.Date;
                entity.Name = model.Name;
                entity.Location = model.Location;
                entity.Distance = model.Distance;
                entity.Description = model.Description;
                entity.Comments = model.Comments;
                entity.CompletionTime = model.CompletionTime;
                entity.ShoeID = model.ShoeID;

                return _db.SaveChanges() == 1;
            }
        }

        // Delete Any Race
        public bool DeleteRace(int id)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                        .Races
                        .Single(r => r.RaceID == id && r.UserID == _userID);
                _db.Races.Remove(entity);

                return _db.SaveChanges() == 1;
            }
        }
    }
}
