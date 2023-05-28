using Microsoft.AspNetCore.Mvc;
using RentMyRide.DataAcess.Repository.IRepository;
using RentMyRide.Models.ViewModels;
using RentMyRide.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentMyRide.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public IActionResult Update(int CarId, int? ReservationId)
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
                currentCar = _UnitOfWork.Car.GetFirstOrDefault(u => u.Id == CarId),
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
            //Update 
            reservationVM.reservation = _UnitOfWork.Reservation.GetFirstOrDefault(u => u.Id == ReservationId);
            reservationVM.Reservation_Services = _UnitOfWork.ReservationServices.GetAll().Where(u => u.ReservationId == reservationVM.reservation.Id);
            return View(reservationVM);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ReservationVM obj, IFormCollection form)
        {
            //obj.reservation.ApplicationUserId=obj.
            if (ModelState.IsValid)
            {
                _UnitOfWork.Reservation.Update(obj.reservation);
                TempData["success"] = "reservation updated successfuly";
                _UnitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Get
        public IActionResult Remove(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var CurrentReservationService = _UnitOfWork.ReservationServices.GetFirstOrDefault(u => u.id == Id, includeProperties: "AdditionalService,Reservation");
            var CurrentCarID = CurrentReservationService.Reservation.CarId;
            if (CurrentReservationService == null)
            {
                return NotFound();
            }
            return View(CurrentReservationService);
        }
        //Post
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public IActionResult RemovePOST(int? Id)
        {

            var Obj = _UnitOfWork.ReservationServices.GetFirstOrDefault(u => u.id == Id, includeProperties: "AdditionalService,Reservation");
            var CurrentCarID = Obj.Reservation.CarId;
            if (Obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.ReservationServices.Remove(Obj);
            _UnitOfWork.Save();
            TempData["success"] = "Reservation Service deleted successfuly";
            return RedirectToAction("Update", "Reservation", new { ReservationId = Obj.ReservationId, CarId = CurrentCarID }); /*, */
        }

        //Get
        public IActionResult CreateAdditionalService(int id)
        {
            var CurrentReservationServices = _UnitOfWork.ReservationServices.GetAll(u => u.ReservationId == id);
            var AdditionalServices = _UnitOfWork.AdditionalService.GetAll();
            List<AdditionalService> NotIncluded = new List<AdditionalService>();

            foreach (var l2 in AdditionalServices)
            {
                bool isIncluded = false;
                foreach (var l1 in CurrentReservationServices)
                {
                    if (l1.AdditionalService.Id == l2.Id)
                    {
                        isIncluded = true;
                        break;
                    }
                }

                if (!isIncluded)
                {
                    NotIncluded.Add(l2);
                }
            }

            return View(NotIncluded);
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAdditionalService(int id, IFormCollection form)
        {

            var CurrentReservation = _UnitOfWork.Reservation.GetFirstOrDefault(u => u.Id == id, includeProperties: "Car");
            var CurrentCarID = CurrentReservation.CarId;

            var AdditionalServices = _UnitOfWork.AdditionalService.GetAll();
            List<AdditionalService> NotIncluded = new List<AdditionalService>();
            var CurrentReservationServices = _UnitOfWork.ReservationServices.GetAll(u => u.ReservationId == id);

            foreach (var l2 in AdditionalServices)
            {
                bool isIncluded = false;
                foreach (var l1 in CurrentReservationServices)
                {
                    if (l1.AdditionalService.Id == l2.Id)
                    {
                        isIncluded = true;
                        break;
                    }
                }

                if (!isIncluded)
                {
                    NotIncluded.Add(l2);
                }
            }

            List<int> checkedIds = new List<int>();

            foreach (var item in NotIncluded)
            {
                string checkboxName = $"MyCheckboxes_{item.Id}";
                if (form.TryGetValue(checkboxName, out var value) && value == "true")
                {
                    checkedIds.Add(item.Id);

                    Reservation_Services reservation_Services = new Reservation_Services
                    {
                        ReservationId = id,
                        AdditionalServiceId = item.Id
                    };

                    _UnitOfWork.ReservationServices.Add(reservation_Services);
                    _UnitOfWork.Save();
                    TempData["success"] = "Additional service added successfully";
                }
            }
            return RedirectToAction("Update", "Reservation", new { ReservationId = id, CarId = CurrentCarID }); /*, */

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var List = _UnitOfWork.Reservation.GetAll(includeProperties: "ApplicationUser,Car");
            return Json(new { data = List });
        }
        #endregion

    }
}
