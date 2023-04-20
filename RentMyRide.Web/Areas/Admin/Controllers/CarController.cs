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
    public class CarController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CarController(IUnitOfWork UnitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _UnitOfWork = UnitOfWork;
            _hostEnvironment = hostEnvironment;

        }

        public IActionResult Index()
        {
            return View();
        }
        //Get
        public IActionResult Upsert(int? Id)
        {
            CarVM carVM = new() {
                car = new(),
                LocationList = _UnitOfWork.Location.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            
            if (Id == null || Id == 0)
            {
                //Create 
                return View(carVM);
            }
            else
            {
                //Update 
                carVM.car = _UnitOfWork.Car.GetFirstOrDefault(u => u.Id == Id);
                return View(carVM);
            }

        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CarVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    //Generate new file name
                    string FileName = Guid.NewGuid().ToString();
                    //find the location where the files should be uploaded
                    var uploads = Path.Combine(wwwRootPath, @"images\cars");
                    //keep same extension
                    var extension = Path.GetExtension(file.FileName);

                    //update the image - Check if there is an image
                    if (obj.car.img != null)
                    {
                        //old image path
                        var oldImagePath = Path.Combine(wwwRootPath, obj.car.img.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    //copy the file uploaded inside the product folder
                    using (var fileStreams = new FileStream(Path.Combine(uploads, FileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    //what we will save in the DB
                    obj.car.img = @"\images\cars\" + FileName + extension;
                }

                if (obj.car.Id == 0)
                {
                    _UnitOfWork.Car.Add(obj.car);
                    TempData["success"] = "Car created successfuly";
                }
                else
                {
                    _UnitOfWork.Car.Update(obj.car);
                    TempData["success"] = "Car updated successfuly";

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
            var List = _UnitOfWork.Car.GetAll(includeProperties: "Location");
            return Json(new { data = List });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var Obj = _UnitOfWork.Car.GetFirstOrDefault(u => u.Id == Id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _UnitOfWork.Car.Remove(Obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
