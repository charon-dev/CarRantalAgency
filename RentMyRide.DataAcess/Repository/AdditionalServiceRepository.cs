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
    public class AdditionalServiceRepository : Repository<AdditionalService>, IAdditionalServiceRepository
    {
        private ApplicationDbContext _db;
        public AdditionalServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(AdditionalService obj)
        {
            _db.AdditionalServices.Update(obj);
            
        }
    }

}
