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
    public class RentingRepository : Repository<Renting>, IRentingRepository
    {
        private ApplicationDbContext _db;
        public RentingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Renting obj)
        {
            _db.Renting.Update(obj);
            
        }
    }

}
