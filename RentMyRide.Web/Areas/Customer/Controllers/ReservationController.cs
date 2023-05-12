using Microsoft.AspNetCore.Mvc;

namespace RentMyRide.Web.Areas.Customer.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
