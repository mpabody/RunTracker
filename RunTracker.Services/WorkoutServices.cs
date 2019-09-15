using BlueBadgeRunTracker.Data;
using RunTracker.Data;
using RunTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Services
{
    public class WorkoutService
    {
        private readonly Guid _userID;

        public WorkoutService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateWorkout(WorkoutCreate model)
        {
            var entity =
                new Workout()
                {
                    UserID = _userID,
                    Date = model.Date,
                    Distance = model.Distance,
                    CompletionTime = model.CompletionTime,
                    Comments = model.Comments
                };

            using (var _db = new ApplicationDbContext())
            {
                _db.Workouts.Add(entity);
                return _db.SaveChanges() == 1;
            }
        }
    }
}
