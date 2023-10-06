using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class CountryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var countries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");
            foreach (var item in countries.Item1.countries)
            {
                item.flagImgUrl = ApiCaller<Country,Country>.Base_Url.Substring(0,
                    ApiCaller<Country, Country>.Base_Url.Length-5) + item.flagImgUrl;
            }
            return View(countries.Item1.countries);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Create([FromForm] Country country)
        {
            try
            {

                

                var response = await ApiCaller<defaultResponse, Country>.CallApiPostCountryFlag($"Countries/AddCountry", country, "");
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

        public async Task<ActionResult> Delete(int id)
        {
            var contact_result = await ApiCaller<GetCountry, string>.CallApiGet($"Countries/GetCountryById?Id={id}", "", "");
            Country country = contact_result.Item1.country;
            if (country != null)
            {
                return View(country);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Countries/DeleteCountry?id={id}", "", "");
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var contact_result = await ApiCaller<GetCountry, string>.CallApiGet($"Countries/GetCountryById?Id={id}", "", "");
            Country country = contact_result.Item1.country;
            if (country != null)
            {
                return View(country);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Country country)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Country>.CallApiPutCountryFlag($"Countries/EditCountry", country, "");
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
