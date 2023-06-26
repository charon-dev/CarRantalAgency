using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentMyRide.DataAcess.Repository;
using RentMyRide.DataAcess.Repository.IRepository;
using RentMyRide.Models;
using RentMyRide.Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace RentMyRide.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork UnitOfWork)
        {
            _logger = logger;
            _UnitOfWork = UnitOfWork;

        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                CarsCount = _UnitOfWork.Car.GetAll().Count(),
                UsersCount = _UnitOfWork.ApplicationUser.GetAll().Count(),
                LocationsList = _UnitOfWork.Location.GetAll(),
                RentingCount = _UnitOfWork.Renting.GetAll().Count(),
                OpenHours = "Monday-Friday: 9am-6pm, Saturday: 10am-4pm, Sunday: Closed",
                Cars =_UnitOfWork.Car.GetAll(u=>u.Year== "2023"),
                Feedback=new()
            };
            return View(homeVM);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Index(HomeVM homeVM)
        {
            var claimsIDentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIDentity.FindFirst(ClaimTypes.NameIdentifier);
            if (homeVM.Feedback.Comment!=null)
            {
                homeVM.Feedback.ApplicationUserId = claim.Value;
                _UnitOfWork.FeedBack.Add(homeVM.Feedback);
                TempData["success"] = "Thanks for sharing your feedback";
                _UnitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Error: Feedback is empty";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}