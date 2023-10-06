using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class CountryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var contries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");

            return View(contries.Item1.countries);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Country country)
        {
            try
            {

                var response = await ApiCaller<defaultResponse, Country>.CallApiPostFile($"Countries/AddCountry", country, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

    }
}
