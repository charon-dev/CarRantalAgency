using RentMyRide.DataAcess.Data;
using RentMyRide.DataAcess.Repository.IRepository;
using RentMyRide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.DataAcess.Repository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private ApplicationDbContext _db;
        public LocationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Location obj)
        {
            _db.Locations.Update(obj);
            
        }
    }

}
