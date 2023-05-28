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
        // GET
        public IActionResult Index(int? page, List<string> makeFilter, string gearboxFilter, List<string> colorsFilter, List<string> locationFilter, double price, int places)
        {
            int pageSize = 5; // Number of cars to display per page
            int pageNumber = page ?? 1; // Current page number

            var carList = _UnitOfWork.Car.GetAll(includeProperties: "Location");
            var completedRentings = _UnitOfWork.Renting.GetAll(u => u.Status == "Completed", includeProperties: "Car");
            var completedReservations = _UnitOfWork.Reservation.GetAll(u => u.Status == "Completed" || u.Status == "Cancelled", includeProperties: "Car");

            // Apply filters
            var filteredCars = carList;

            if (makeFilter != null && makeFilter.Any())
            {
                filteredCars = filteredCars.Where(c => makeFilter.Contains(c.Make));
            }
            if (!string.IsNullOrEmpty(gearboxFilter))
            {
                filteredCars = filteredCars.Where(c => c.Gearbox == gearboxFilter);
            }

            if (colorsFilter != null && colorsFilter.Any())
            {
                filteredCars = filteredCars.Where(c => colorsFilter.Contains(c.Color));
            }

            if (locationFilter != null && locationFilter.Any())
            {
                filteredCars = filteredCars.Where(c => locationFilter.Contains(c.Location.Name));
            }

            if (price > 0)
            {
                filteredCars = filteredCars.Where(c => c.PriceByDay <= price);
            }

            if (places > 0)
            {
                filteredCars = filteredCars.Where(c => c.Places == places);
            }

            var availableCars = filteredCars.Where(c =>
     (!_UnitOfWork.Renting.GetAll().Any(r => r.CarId == c.Id && r.Status == "Completed")
     && (!_UnitOfWork.Reservation.GetAll().Any(res => res.CarId == c.Id && (res.Status == "Completed" || res.Status == "Cancelled"))))
     || (_UnitOfWork.Reservation.GetAll().Any(res => res.CarId == c.Id && (res.Status == "Completed" || res.Status == "Cancelled")))
     || (_UnitOfWork.Renting.GetAll().Any(r => r.CarId == c.Id && r.Status == "Completed"))
 ).ToList();




            var makes = carList.Select(c => c.Make).Distinct().ToList();
            var colors = carList.Select(c => c.Color).Distinct().ToList();
            var gearboxes = carList.Select(c => c.Gearbox).Distinct().ToList();

            var pagedCars = availableCars.ToPagedList(pageNumber, pageSize);

            AvailableCarsVM availableCarsVM = new()
            {
                cars = availableCars,
                LocationsList = _UnitOfWork.Location.GetAll(),
                MakesList = makes,
                ColorsList = colors,
                GearboxesList = gearboxes,
                Places = places,
                Price = price,
                PagedCars = pagedCars,
                SelectedMakeFilters = makeFilter,
                SelectedGearboxFilter = gearboxFilter,
                SelectedColorsFilters = colorsFilter,
                SelectedLocationFilters = locationFilter
            };

            return View(availableCarsVM);
        }



    }
}
