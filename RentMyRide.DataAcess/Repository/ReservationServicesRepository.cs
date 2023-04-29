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
    public class ReservationServicesRepository : Repository<Reservation_Services>, IReservationServicesRepository
    {
        private ApplicationDbContext _db;
        public ReservationServicesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Reservation_Services obj)
        {
            _db.Reservation_Services.Update(obj);
            
        }
    }

}
