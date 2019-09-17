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
                    Comments = model.Comments,
                    ShoeID = model.ShoeID
                };

            using (var _db = new ApplicationDbContext())
            {
                _db.Workouts.Add(entity);
                return _db.SaveChanges() == 1;
            }
        }

        public IEnumerable<WorkoutListItem> GetWorkouts()
        {
            using (var _db = new ApplicationDbContext())
            {
                var query = _db.Workouts
                    .Where(w => w.UserID == _userID)
                    .Select(
                        w =>
                            new WorkoutListItem
                            {
                                ID = w.ID,
                                Date = w.Date,
                                Distance = w.Distance
                            }
                    );
                return query.ToList();
            }
        }

        public WorkoutDetail GetWorkoutByID(int id)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                   _db
                       .Workouts
                       .Single(w => w.ID == id && w.UserID == _userID);
                return
                    new WorkoutDetail
                    {
                        ID = entity.ID,
                        Date = entity.Date,
                        Distance = entity.Distance,
                        CompletionTime = entity.CompletionTime,
                        Comments = entity.Comments
                    };
            }

        }
        public bool UpdateWorkout(WorkoutEdit model)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                        .Workouts
                        .Single(w => w.ID == model.ID && w.UserID == _userID);

                entity.Date = model.Date;
                entity.Distance = model.Distance;
                entity.CompletionTime = model.CompletionTime;
                entity.Comments = model.Comments;

                return _db.SaveChanges() == 1;
            }
        }

        public bool DeleteWorkout(int id)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                        .Workouts
                        .Single(w => w.ID == id && w.UserID == _userID);
                _db.Workouts.Remove(entity);

                return _db.SaveChanges() == 1;
            }
        }
    }
}
