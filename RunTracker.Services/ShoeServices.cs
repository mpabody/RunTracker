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
    public class ShoeService
    {
        private readonly Guid _userID;

        public ShoeService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateShoe(ShoeCreate model)
        {
            var entity =
                new Shoe()
                {
                    UserID = _userID,
                    Brand = model.Brand,
                    Name = model.Name,
                    Description = model.Description
                };

            using (var _db = new ApplicationDbContext())
            {
                _db.Shoes.Add(entity);
                return _db.SaveChanges() == 1;
            }
        }

        public IEnumerable<ShoeListItem> GetShoes()
        {
            using (var _db = new ApplicationDbContext())
            {
                var query = _db.Shoes
                    .Where(s => s.UserID == _userID)
                    .Select(
                        s =>
                            new ShoeListItem
                            {
                                ShoeID = s.ShoeID,
                                Name = s.Name,
                                Workouts = _db.Workouts.Where(w => w.ShoeID == s.ShoeID).ToList(),
                                Races = _db.Races.Where(r => r.ShoeID == s.ShoeID).ToList()
                            }
                        );
                return query.ToList();
            }
        }

        public ShoeDetail GetShoeByID(int id)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                        .Shoes
                        .Single(s => s.ShoeID == id && s.UserID == _userID);

                return
                    new ShoeDetail  //                                                  ICollections
                    {
                        ShoeID = entity.ShoeID,
                        Brand = entity.Brand,
                        Name = entity.Name,
                        Description = entity.Description,
                        Workouts = _db.Workouts.Where(w => w.ShoeID == entity.ShoeID).ToList(),
                        Races = _db.Races.Where(r => r.ShoeID == entity.ShoeID).ToList()
                    };
            }
        }

        public bool UpdateShoe(ShoeEdit model)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                        .Shoes
                        .Single(s => s.ShoeID == model.ShoeID && s.UserID == _userID);

                entity.Brand = model.Brand;
                entity.Name = model.Name;
                entity.Description = model.Description;

                return _db.SaveChanges() == 1;
            }
        }

        public bool DeleteShoe(int id)
        {
            using (var _db = new ApplicationDbContext())
            {
                var entity =
                    _db
                        .Shoes
                        .Single(s => s.ShoeID == id && s.UserID == _userID);

                _db.Shoes.Remove(entity);

                return _db.SaveChanges() == 1;
            }
        }
    }
}
