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
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        private ApplicationDbContext _db;
        public ReservationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Reservation obj)
        {
            _db.Reservations.Update(obj);
            
        }
    }

}
