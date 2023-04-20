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
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public EmployeeController(IUnitOfWork UnitOfWork)
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
            EmployeeVM employeeVM = new() {
                employee = new(),
                LocationList = _UnitOfWork.Location.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            
            if (Id == null || Id == 0)
            {
                //Create 
                return View(employeeVM);
            }
            else
            {
                //Update 
                employeeVM.employee = _UnitOfWork.Employee.GetFirstOrDefault(u => u.Id == Id);
                return View(employeeVM);
            }

        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(EmployeeVM obj)
        {

            if (ModelState.IsValid)
            {                
                if (obj.employee.Id == 0)
                {
                    _UnitOfWork.Employee.Add(obj.employee);
                    TempData["success"] = "Employee created successfuly";
                }
                else
                {
                    _UnitOfWork.Employee.Update(obj.employee);
                    TempData["success"] = "Employee updated successfuly";

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
            var List = _UnitOfWork.Employee.GetAll(includeProperties: "Location");
            return Json(new { data = List });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var Obj = _UnitOfWork.Employee.GetFirstOrDefault(u => u.Id == Id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _UnitOfWork.Employee.Remove(Obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
