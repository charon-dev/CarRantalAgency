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
    public class RentingServicesRepository : Repository<Renting_Services>, IRentingServicesRepository
    {
        private ApplicationDbContext _db;
        public RentingServicesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Renting_Services obj)
        {
            _db.Renting_Services.Update(obj);
            
        }
    }

}
