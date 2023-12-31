﻿using LiperFrontend.Models;
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
            try
            {
                var cities = await ApiCaller<Cities, string>.CallApiGet("cities", "", "");
                if (cities.cities is not null)
                {
                    return View(cities.cities);
                }
                return View(new List<City>());
            }
            catch (Exception ex)
            {
                return View(new List<City>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Retrieve latitude and longitude values from your data source
            try
            {
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
                var statesList = states.states;
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
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult SendLocation(float latitude, float longitude)
        {
            try
            {
                HttpContext.Session.SetString(SessionLat, latitude.ToString());
                HttpContext.Session.SetString(SessionLong, longitude.ToString());

                var location = new City
                {
                    latitude = latitude,
                    longitude = longitude
                };

                HttpContext.Session.SetInt32("Alert", 1);

                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(City city)
        {
            try
            {
                var lat = HttpContext.Session.GetString(SessionLat);
                var lon = HttpContext.Session.GetString(SessionLong);
                HttpContext.Session.Remove(SessionLat);
                HttpContext.Session.Remove(SessionLong);
                if (lat is not null && lon is not null)
                {
                    city.latitude = float.Parse(lat);
                    city.longitude = float.Parse(lon);
                    var response = await ApiCaller<defaultResponse, City>.CallApiPost($"cities/Addcity", city, "");
                    responseMessage responseMessage = response.responseMessage;
                    if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Warning, "Select City Location first!");
                }
                List<SelectListItem> statesSelectedList = new List<SelectListItem>();
                var states = await ApiCaller<States, string>.CallApiGet("states", "", "");
                var statesList = states.states;
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
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await ApiCaller<GetCity, string>.CallApiGet($"Cities/GetCityById?Id={id}", "", "");
                City city = result.city;
                if (city != null)
                {
                    return View(city);
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
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"cities/DeleteCity?id={id}", "", "");
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, response.responseMessage.messageEN);
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

            try
            {
                var result = await ApiCaller<GetCity, string>.CallApiGet($"Cities/GetCityById?Id={id}", "", "");
                City city = result.city;
                if (city != null)
                {
                    List<SelectListItem> statesSelectedList = new List<SelectListItem>();
                    var states = await ApiCaller<States, string>.CallApiGet("states", "", "");
                    var statesList = states.states;
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
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(City city)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, City>.CallApiPut($"Cities/EditCity", city, "");
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");

                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");

                }
                List<SelectListItem> statesSelectedList = new List<SelectListItem>();
                var states = await ApiCaller<States, string>.CallApiGet("states", "", "");
                var statesList = states.states;
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