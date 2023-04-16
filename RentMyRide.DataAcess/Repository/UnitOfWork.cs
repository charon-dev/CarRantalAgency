using RentMyRide.DataAcess.Data;
using RentMyRide.DataAcess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.DataAcess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IAdditionalServiceRepository AdditionalService { get; private set; }
        public ICarRepository Car { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public IFeedbackRepository FeedBack { get; private set; }

        public ILocationRepository Location { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IRentingRepository Renting { get; private set; }
        public IReservationRepository Reservation { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            AdditionalService = new AdditionalServiceRepository(_db);
            Car = new CarRepository(_db);
            Employee = new EmployeeRepository(_db);
            FeedBack = new FeedbackRepository(_db);
            Location = new LocationRepository(_db);
            Renting = new RentingRepository(_db);
            Reservation = new ReservationRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
