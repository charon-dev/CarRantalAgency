using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using RentMyRide.DataAcess.Repository.IRepository;
using RentMyRide.Models;
using RentMyRide.Utility;
using System.Data;
using Location = RentMyRide.Models.Location;

namespace RentMyRide.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AdditionalServiceController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public AdditionalServiceController(IUnitOfWork UnitOfWork)
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
            AdditionalService location = new();
            if (Id == null || Id == 0)
            {
                //Create 
                return View(location);
            }
            else
            {
                //Update 
                location = _UnitOfWork.AdditionalService.GetFirstOrDefault(u => u.Id == Id);
                return View(location);
            }

        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(AdditionalService obj)
        {

            if (ModelState.IsValid)
            {                
                if (obj.Id == 0)
                {
                    _UnitOfWork.AdditionalService.Add(obj);
                    TempData["success"] = "Additional service created successfuly";
                }
                else
                {
                    _UnitOfWork.AdditionalService.Update(obj);
                    TempData["success"] = "Additional service updated successfuly";

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
            var List = _UnitOfWork.AdditionalService.GetAll();
            return Json(new { data = List });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var Obj = _UnitOfWork.AdditionalService.GetFirstOrDefault(u => u.Id == Id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _UnitOfWork.AdditionalService.Remove(Obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
