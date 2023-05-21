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
        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var claimsIDentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIDentity.FindFirst(ClaimTypes.NameIdentifier);
            var List = _UnitOfWork.Renting.GetAll(u=>u.ApplicationUserId==claim.Value,includeProperties: "ApplicationUser,Car");
            return Json(new { data = List });
        }
        
        #endregion
    }
}
