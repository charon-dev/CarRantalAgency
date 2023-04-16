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
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private ApplicationDbContext _db;
        public CarRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Car obj)
        {
            _db.Cars.Update(obj);
            
        }
    }

}
