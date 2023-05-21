using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentMyRide.Models.ViewModels;
using RentMyRide.Models;
using RentMyRide.DataAcess.Repository.IRepository;
using System.Security.Claims;
using RentMyRide.DataAcess.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace RentMyRide.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public ReservationController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Get
        public IActionResult Upsert(int CarId, int? ReservationId)
        {
            var status = new List<string>()
            {
                "Active",
                "Pending",
                "Completed",
                "Cancelled"
            };
            ReservationVM reservationVM = new()
            {
                reservation = new(),
                AdditionalServices = _UnitOfWork.AdditionalService.GetAll(),
                Reservation_Services = new List<Reservation_Services>(),
                currentCar=_UnitOfWork.Car.GetFirstOrDefault(u=>u.Id==CarId),
                Users = _UnitOfWork.ApplicationUser.GetAll().Select(i => new SelectListItem
                {
                    Text = $"{i.FirstName} {i.LastName}",
                    Value = i.Id.ToString()
                }),
                StatusList = status.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };
            return View(reservationVM);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ReservationVM obj, IFormCollection form)
        {
            if (!User.IsInRole("Admin"))
            {
                var claimsIDentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIDentity.FindFirst(ClaimTypes.NameIdentifier);
                obj.reservation.ApplicationUserId = claim.Value;
            }
            

            if (ModelState.IsValid)
            {
                //Create renting
                _UnitOfWork.Reservation.Add(obj.reservation);
                _UnitOfWork.Save();
                TempData["success"] = "Reservation created successfuly";

                //Create Renting_Services
                List<int> checkedIds = new List<int>();
                obj.AdditionalServices = _UnitOfWork.AdditionalService.GetAll();
                foreach (var item in obj.AdditionalServices)
                {
                    string checkboxName = $"MyCheckboxes_{item.Id}";
                    if (form.TryGetValue(checkboxName, out var value) && value == "true")
                    {
                        checkedIds.Add(item.Id);

                        Reservation_Services reservation_Services = new Reservation_Services
                        {
                            ReservationId = obj.reservation.Id,
                            AdditionalServiceId = item.Id
                        };

                        _UnitOfWork.ReservationServices.Add(reservation_Services);
                        _UnitOfWork.Save();
                        TempData["success"] = "Additional service added successfuly";

                    }
                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var claimsIDentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIDentity.FindFirst(ClaimTypes.NameIdentifier);
            var List = _UnitOfWork.Reservation.GetAll(u=>u.ApplicationUserId==claim.Value,includeProperties: "ApplicationUser,Car");
            return Json(new { data = List });
        }
        
        #endregion
    }
}
