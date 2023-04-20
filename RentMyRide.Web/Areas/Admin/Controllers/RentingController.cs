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
            RentingVM rentingVM = new() {
                renting = new(),
                CarList = _UnitOfWork.Car.GetAll().Select(i => new SelectListItem
                {
                    Text = $"{i.Make} {i.Model}| {i.LicensePlateNumber}",
                    Value = i.Id.ToString()
                }),
                UsersList = _UnitOfWork.ApplicationUser.GetAll().Select(i => new SelectListItem
                {
                    Text = $"{i.FirstName} {i.LastName}",
                    Value = i.Id.ToString()
                })
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
                return View(rentingVM);
            }

        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(RentingVM obj)
        {

            if (ModelState.IsValid)
            {                
                if (obj.renting.Id == 0)
                {
                    obj.renting.TotalCharge = obj.renting.RentalFees;
                    _UnitOfWork.Renting.Add(obj.renting);
                    TempData["success"] = "renting created successfuly";
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
