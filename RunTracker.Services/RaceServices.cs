using BlueBadgeRunTracker.Data;
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


    }
}
