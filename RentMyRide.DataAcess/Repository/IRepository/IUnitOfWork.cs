using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.DataAcess.Repository.IRepository
{
    public interface IUnitOfWork
    {

        IApplicationUserRepository ApplicationUser { get; }
        IAdditionalServiceRepository AdditionalService { get; }
        ICarRepository Car { get; }
        IEmployeeRepository Employee { get; }
        IFeedbackRepository FeedBack { get; }
        ILocationRepository Location { get; }
        IRentingRepository Renting { get; }
        IReservationRepository Reservation { get; }
        IRentingServicesRepository RentingServices { get; }
        IReservationServicesRepository ReservationServices { get; }


        void Save();
    }
}
