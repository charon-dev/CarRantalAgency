using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using RentMyRide.DataAcess.Repository.IRepository;
using RentMyRide.Models;
using RentMyRide.Models.ViewModels;
using RentMyRide.Utility;
using System.Data;
using Location = RentMyRide.Models.Location;

namespace RentMyRide.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class RentingController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public RentingController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        //Get
        public IActionResult Upsert(int? Id)
        {
            var status = new List<string>()
            {
                "Active",
                "Pending",
                "Completed"
            };
            var Payments = new List<string>()
            {
                "Cash",
                "Bank transfer"
            };
            RentingVM rentingVM = new() {
                renting = new(),
                CarList = _UnitOfWork.Car.GetAll().Select(i => new SelectListItem
                {
                    Text = $"{i.Make} {i.Model} | {i.LicensePlateNumber}",
                    Value = i.Id.ToString()
                }),
                UsersList = _UnitOfWork.ApplicationUser.GetAll().Select(i => new SelectListItem
                {
                    Text = $"{i.FirstName} {i.LastName}",
                    Value = i.Id.ToString()
                }),
                StatusList = status.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                }),
                PaymentList = Payments.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                }),
                AdditionalServices = _UnitOfWork.AdditionalService.GetAll(),
                Renting_Services = new List<Renting_Services>()
            };
            
            if (Id == null || Id == 0)
            {
                //Create 
                return View(rentingVM);
            }
            else
            {
                //Update 
                rentingVM.renting = _UnitOfWork.Renting.GetFirstOrDefault(u => u.Id == Id);
                rentingVM.Renting_Services = _UnitOfWork.RentingServices.GetAll().Where(u => u.RentingId == rentingVM.renting.Id);
                return View(rentingVM);
            }

        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(RentingVM obj, IFormCollection form)
        {

            if (ModelState.IsValid)
            {                
                if (obj.renting.Id == 0)
                {
                    
                    //Create renting
                    obj.renting.TotalCharge = obj.renting.RentalFees;
                    _UnitOfWork.Renting.Add(obj.renting);
                    _UnitOfWork.Save();
                    TempData["success"] = "renting created successfuly";

                    //Create Renting_Services
                    List<int> checkedIds = new List<int>();
                    obj.AdditionalServices = _UnitOfWork.AdditionalService.GetAll();
                    foreach (var item in obj.AdditionalServices)
                    {
                        string checkboxName = $"MyCheckboxes_{item.Id}";
                        if (form.TryGetValue(checkboxName, out var value) && value == "true")
                        {
                            checkedIds.Add(item.Id);

                            Renting_Services renting_Services = new Renting_Services
                            {
                                RentingId = obj.renting.Id,
                                AdditionalServiceId = item.Id
                            };
                                                    
                            _UnitOfWork.RentingServices.Add(renting_Services);
                            _UnitOfWork.Save();
                            var Renting_servicesPrice = _UnitOfWork.RentingServices.GetFirstOrDefault(u => u.id == renting_Services.id, includeProperties: "AdditionalService").AdditionalService.Price;
                            var currentRenting = _UnitOfWork.Renting.GetFirstOrDefault(u => u.Id == obj.renting.Id, includeProperties: "Car");
                            //Caluclate number of days :
                            DateTime firstDate = currentRenting.StartDate;
                            DateTime secondDate = currentRenting.EndDate;

                            TimeSpan difference = secondDate - firstDate;
                            int days = difference.Days;

                            currentRenting.RentalFees = currentRenting.Car.PriceByDay * days;
                            currentRenting.ExtraCharge += renting_Services.AdditionalService.Price;
                            currentRenting.TotalCharge = currentRenting.RentalFees + currentRenting.ExtraCharge;
                            _UnitOfWork.Renting.Update(currentRenting);
                            _UnitOfWork.Save();
                            TempData["success"] = "Additional service added successfuly";

                        }
                    }
                    
                }
                else
                {
                    _UnitOfWork.Renting.Update(obj.renting);
                    TempData["success"] = "renting updated successfuly";

                }
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
            var CurrentRentingService = _UnitOfWork.RentingServices.GetFirstOrDefault(u => u.id == Id, includeProperties: "AdditionalService");
            if (CurrentRentingService == null)
            {
                return NotFound();
            }
            return View(CurrentRentingService);
        }
        //Post
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public IActionResult RemovePOST(int? Id)
        {
            
            var Obj = _UnitOfWork.RentingServices.GetFirstOrDefault(u => u.id == Id, includeProperties: "AdditionalService");
            var CurrentRenting = _UnitOfWork.Renting.GetFirstOrDefault(u => u.Id == Obj.RentingId);
            if (Obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.RentingServices.Remove(Obj);
            CurrentRenting.ExtraCharge -= Obj.AdditionalService.Price;
            CurrentRenting.TotalCharge -= Obj.AdditionalService.Price;
            _UnitOfWork.Renting.Update(CurrentRenting);
            _UnitOfWork.Save();
            TempData["success"] = "Renting Service deleted successfuly";
            return RedirectToAction("Upsert","Renting", new { Id = Obj.RentingId });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var List = _UnitOfWork.Renting.GetAll(includeProperties: "ApplicationUser,Car");
            return Json(new { data = List });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var Obj = _UnitOfWork.Renting.GetFirstOrDefault(u => u.Id == Id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _UnitOfWork.Renting.Remove(Obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
