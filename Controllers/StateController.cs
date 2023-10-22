using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiperFrontend.Controllers
{
    public class StateController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var states = await ApiCaller<States, string>.CallApiGet("states", "", "");
                if (states.states is not null)
                {
                    return View(states.states);
                }
                return View(new List<State>());
            }
            catch (Exception ex)
            {
                return View(new List<State>());
            }
        }

        public async Task<ActionResult> Create()
        {
            try
            {
                List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
                var countries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");
                var countriesList = countries.countries;
                foreach (var country in countriesList)
                {
                    var selectItem = new SelectListItem() { Value = country.Id.ToString(), Text = country.NameEN };
                    countriesSelectedList.Add(selectItem);
                }
                ViewBag.countriesSelectedList = countriesSelectedList;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(State state)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, State>.CallApiPost($"states/Addstate", state, "");
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");
                    
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");
                    
                }
                List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
                var countries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");
                var countriesList = countries.countries;
                foreach (var country in countriesList)
                {
                    var selectItem = new SelectListItem() { Value = country.Id.ToString(), Text = country.NameEN };
                    if(selectItem.Value==state.countryId.ToString())
                        selectItem.Selected = true;
                    countriesSelectedList.Add(selectItem);
                }
                ViewBag.countriesSelectedList = countriesSelectedList;
                return View();
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
                var result = await ApiCaller<GetState, string>.CallApiGet($"states/GetStateById?Id={id}", "", "");
                State state = result.state;
                if (state != null)
                {
                    return View(state);
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
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"states/DeleteState?id={id}", "", "");
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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

        public async Task<ActionResult> Edit(int id)
        {

            try
            {
                var result = await ApiCaller<GetState, string>.CallApiGet($"states/GetStateById?Id={id}", "", "");
                State state = result.state;
                if (state != null)
                {
                    List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
                    var countries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");
                    var countriesList = countries.countries;
                    foreach (var country in countriesList)
                    {
                        var selectItem = new SelectListItem() { Value = country.Id.ToString(), Text = country.NameEN };
                        if (selectItem.Value == state.countryId.ToString())
                            selectItem.Selected = true;
                        countriesSelectedList.Add(selectItem);
                    }
                    ViewBag.countriesSelectedList = countriesSelectedList;
                    return View(state);
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
        public async Task<ActionResult> Edit(State state)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, State>.CallApiPut($"states/EditState", state, "");
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");

                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");
                    
                }
                List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
                var countries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");
                var countriesList = countries.countries;
                foreach (var country in countriesList)
                {
                    var selectItem = new SelectListItem() { Value = country.Id.ToString(), Text = country.NameEN };
                    if (selectItem.Value == state.countryId.ToString())
                        selectItem.Selected = true;
                    countriesSelectedList.Add(selectItem);
                }
                ViewBag.countriesSelectedList = countriesSelectedList;
                return View();
            }
            catch
            {
                return View(state);
            }
        }
    }
}
