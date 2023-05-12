using Microsoft.AspNetCore.Mvc;
using RentMyRide.DataAcess.Repository.IRepository;
using RentMyRide.Models;
using RentMyRide.Models.ViewModels;

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
        public IActionResult Index(DateTime? StartDate, DateTime? EndDate, string? location)
        {
            var carList = _UnitOfWork.Car.GetAll(includeProperties: "Location");
            var Makes=new List<string>();
            var Colors = new List<string>();
            var Gearboxes = new List<string>();
            foreach (var item in carList)
            {
                Makes.Add(item.Make);
                Colors.Add(item.Color);
                Gearboxes.Add(item.Gearbox);
            }
            AvailableCarsVM availableCarsVM = new()
            {
                cars = carList,
                LocationsList = _UnitOfWork.Location.GetAll(),
                MakesList = Makes,
                ColorsList = Colors.Distinct().ToList(),
                GearboxesList = Gearboxes.Distinct().ToList(),
                Places = 0,
                Price=0
            };
            return View(availableCarsVM);
        }
        
    }
}
