using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentMyRide.DataAcess.Repository.IRepository;
using RentMyRide.Models;
using RentMyRide.Models.ViewModels;
using X.PagedList;

namespace RentMyRide.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AvailableCarsController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public AvailableCarsController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        //GET
        public IActionResult Index(int? page)
        {
            int pageSize = 5; // Number of cars to display per page
            int pageNumber = page ?? 1; // Current page number

            var carList = _UnitOfWork.Car.GetAll(includeProperties: "Location");
            var CompletedRentings = _UnitOfWork.Renting.GetAll(includeProperties: "Car");

            var availableCars = (from c in carList
                                 join r in CompletedRentings on c.Id equals r.CarId into rGroup
                                 from r in rGroup.DefaultIfEmpty()
                                 where r == null || r.Status == "Completed"
                                 select c).Distinct();

            var Makes = carList.Select(c => c.Make).ToList();
            var Colors = carList.Select(c => c.Color).Distinct().ToList();
            var Gearboxes = carList.Select(c => c.Gearbox).Distinct().ToList();

            var pagedCars = availableCars.ToPagedList(pageNumber, pageSize);

            AvailableCarsVM availableCarsVM = new()
            {
                cars = availableCars,
                LocationsList = _UnitOfWork.Location.GetAll(),
                MakesList = Makes,
                ColorsList = Colors,
                GearboxesList = Gearboxes,
                Places = 0,
                Price = 0,
                PagedCars = pagedCars
            };

            return View(availableCarsVM);
        }

    }
}
