using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;
using System.Drawing.Printing;

namespace LiperFrontend.Controllers
{
    public class CountryController : Controller
    {
        public async Task<IActionResult> Index( int pg =1 )
        {
            try
            {
                
                var countries = await ApiCaller<Countries, string>.CallApiGet($"Countries", "", "");
                foreach (var item in countries.Item1.countries)
                {
                    item.flagImgUrl = ApiCaller<Country, Country>.Base_Url_files + item.flagImgUrl;
                }
                if (countries.Item1.countries is not null)
                {
                    return View(countries.Item1.countries);
                }
                return View(new List<Country>());

            }
            catch (Exception ex)
            {
                return View(new List<Country>());
            }
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
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");
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
            try
            {
                var contact_result = await ApiCaller<GetCountry, string>.CallApiGet($"Countries/GetCountryById?Id={id}", "", "");
                Country country = contact_result.Item1.country;
                if (country != null)
                {
                    country.flagImgUrl = ApiCaller<Country, Country>.Base_Url_files + country.flagImgUrl;
                    return View(country);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
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
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var contact_result = await ApiCaller<GetCountry, string>.CallApiGet($"Countries/GetCountryById?Id={id}", "", "");
                Country country = contact_result.Item1.country;
                if (country != null)
                {
                    country.flagImgUrl = ApiCaller<Country, Country>.Base_Url.Substring(0,
                        ApiCaller<Country, Country>.Base_Url.Length - 5) + country.flagImgUrl;
                    return View(country);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] Country country)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Country>.CallApiPutCountryFlag($"Countries/EditCountry", country, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");
                    return View(country);
                }
            }
            catch
            {
                return View(country);
            }
        }

    }
}
