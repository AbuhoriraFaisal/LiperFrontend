using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class LiperPolicy : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
