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

        public bool CreateWorkout(RaceInterestedCreate model)
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

        public IEnumerable<RaceInterestedListItem> GetRacesInterested()
        {
            using (var _db = new ApplicationDbContext())
            {
                var query = _db.Races
                    .Where(r => r.UserID == _userID)
                    .Select(
                        r =>
                            new RaceInterestedListItem
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

        public RaceInterestedDetail GetRaceByID(int id)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                    .Races
                    .Single(r => r.RaceID == id && r.UserID == _userID);
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

        public bool UpdateRaceInterested(RaceInterestedEdit model)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                    .Races
                    .Single(r => r.RaceID == model.RaceID && r.UserID == _userID);

                entity.Date = model.Date;
                entity.Name = model.Name;
                entity.Location = model.Location;
                entity.Distance = model.Distance;
                entity.Description = model.Description;
                entity.Comments = model.Comments;

                return _db.SaveChanges() == 1;
            }
        }

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
