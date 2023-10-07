using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using LiperFrontend.Enums;
using LiperFrontend.Services;

namespace LiperFrontend.Controllers
{
    public class CityController : Controller
    {
        const string SessionLat = "lat";
        const string SessionLong = "long";
        public async Task<IActionResult> Index()
        {
            var cities = await ApiCaller<Cities, string>.CallApiGet("cities", "", "");
            return View(cities.Item1.cities);
        }
        [HttpGet]
        public async Task<IActionResult >Create()
        {
            // Retrieve latitude and longitude values from your data source
            var city = new City
            {
                latitude = 10.9f,
                longitude = 2.4194f
            };
            var lat = HttpContext.Session.GetString(SessionLat);
            var lon = HttpContext.Session.GetString(SessionLong);
            if (lat is not null && lon is not null)
            {
                city.latitude = float.Parse(lat);
                city.longitude = float.Parse(lon);
            }
            List<SelectListItem> statesSelectedList = new List<SelectListItem>();
            var states = await ApiCaller<States, string>.CallApiGet("states", "", "");
            var statesList= states.Item1.states;
            foreach (var state in statesList)
            {
                var selectItem = new SelectListItem() { Value = state.Id.ToString(), Text = state.nameEN };
                if (selectItem.Value == state.countryId.ToString())
                    selectItem.Selected = true;
                statesSelectedList.Add(selectItem);
            }
            ViewBag.statesSelectedList = statesSelectedList;

            //add country ddl
            int? alert = HttpContext.Session.GetInt32("Alert");
            HttpContext.Session.Remove("Alert");
            if (alert is not null)
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Info, "location selection Succeeded!");
            return View(city);
        }
        [HttpPost]
        public IActionResult SendLocation(float latitude, float longitude)
        {
            HttpContext.Session.SetString(SessionLat, latitude.ToString());
            HttpContext.Session.SetString(SessionLong, longitude.ToString());

            var location = new City
            {
                latitude =latitude,
                longitude = longitude
            };
            
            HttpContext.Session.SetInt32("Alert", 1);
            
            return RedirectToAction("Create");
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(City city)
        {
            var lat = HttpContext.Session.GetString(SessionLat);
            var lon = HttpContext.Session.GetString(SessionLong);
            HttpContext.Session.Remove(SessionLat);
            HttpContext.Session.Remove(SessionLong);
            if (lat is not null && lon is not null)
            {
                city.latitude = float.Parse( lat);
                city.longitude = float.Parse(lon);
                var response = await ApiCaller<defaultResponse, City>.CallApiPost($"cities/Addcity", city, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");

                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");

                }
            }
            else
            {
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Warning, "Select state first!");
                
            }
            List<SelectListItem> statesSelectedList = new List<SelectListItem>();
            var states = await ApiCaller<States, string>.CallApiGet("states", "", "");
            var statesList = states.Item1.states;
            foreach (var state in statesList)
            {
                var selectItem = new SelectListItem() { Value = state.Id.ToString(), Text = state.nameEN };
                if (selectItem.Value == state.countryId.ToString())
                    selectItem.Selected = true;
                statesSelectedList.Add(selectItem);
            }
            ViewBag.statesSelectedList = statesSelectedList;
            
            return View(city);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var result = await ApiCaller<GetCity, string>.CallApiGet($"Cities/GetCityById?Id={id}", "", "");
            City city = result.Item1.city;
            if (city != null)
            {
                return View(city);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"cities/DeleteCity?id={id}", "", "");
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, response.Item1.responseMessage.messageEN);
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Edit(int id)
        {

            var result = await ApiCaller<GetCity, string>.CallApiGet($"Cities/GetCityById?Id={id}", "", "");
            City city = result.Item1.city;
            if (city != null)
            {
                List<SelectListItem> statesSelectedList = new List<SelectListItem>();
                var states = await ApiCaller<States, string>.CallApiGet("states", "", "");
                var statesList = states.Item1.states;
                foreach (var state in statesList)
                {
                    var selectItem = new SelectListItem() { Value = state.Id.ToString(), Text = state.nameEN };
                    if (selectItem.Value == state.countryId.ToString())
                        selectItem.Selected = true;
                    statesSelectedList.Add(selectItem);
                }
                ViewBag.statesSelectedList = statesSelectedList;
                return View(city);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(City city)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, City>.CallApiPut($"Cities/EditCity", city, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");

                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");

                }
                List<SelectListItem> statesSelectedList = new List<SelectListItem>();
                var states = await ApiCaller<States, string>.CallApiGet("states", "", "");
                var statesList = states.Item1.states;
                foreach (var state in statesList)
                {
                    var selectItem = new SelectListItem() { Value = state.Id.ToString(), Text = state.nameEN };
                    if (selectItem.Value == state.countryId.ToString())
                        selectItem.Selected = true;
                    statesSelectedList.Add(selectItem);
                }
                ViewBag.statesSelectedList = statesSelectedList;
                return View();
            }
            catch
            {
                return View(city);
            }
        }
    }
}
//public async Task<IActionResult> UpdtaestatesListItems(string id)
//{
//    var lat = HttpContext.Session.GetString(SessionLat);
//    var lon = HttpContext.Session.GetString(SessionLong);
//    City city = new City();
//    if (lat is not null && lon is not null)
//    {
//        city.latitude = float.Parse(lat);
//        city.longitude = float.Parse(lon);
//    }
//    List<SelectListItem> statesSelectedList = new List<SelectListItem>();
//    var states = await ApiCaller<States, string>.CallApiGet("states", "", "");
//    var statesList = states.Item1.states;
//    foreach (var state in statesList)
//    {
//        var selectItem = new SelectListItem() { Value = state.Id.ToString(), Text = state.nameEN };
//        if (selectItem.Value == state.countryId.ToString())
//            selectItem.Selected = true;
//        statesSelectedList.Add(selectItem);
//    }
//    ViewBag.countriesSelectedList = statesSelectedList;
//    return View("Create", city);
//}