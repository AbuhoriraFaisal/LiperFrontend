using LiperFrontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class CityController : Controller
    {
        public IActionResult Index()
        {
            // Retrieve latitude and longitude values from your data source
            var location = new City
            {
                latitude = 10.9f,
                longitude = 2.4194f
            };

            return View(location);
        }
    }
}
