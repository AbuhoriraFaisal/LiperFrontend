using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiperFrontend.Controllers
{
    public class CurrencyController : Controller
    {
        // GET: AgentController
        public async Task<ActionResult> Index()
        {
            try
            {
                var currencies = await ApiCaller<Currencies, string>.CallApiGet("Currencies", "", "");
                if (currencies.currencies is not null)
                {
                    return View(currencies.currencies);
                }
                return View(new List<Currency>());
            }
            catch (Exception ex)
            {
                return View(new List<Currency>());
            }
        }


        // GET: AgentController/Create
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
                ViewBag.SelectedList = countriesSelectedList;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: AgentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Currency currency)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Currency>.CallApiPost($"Currencies/AddCurrency", currency, "");
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
                    countriesSelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = countriesSelectedList;
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AgentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var result = await ApiCaller<GetCurrency, string>.CallApiGet($"Currencies/GetCurrencyById?Id={id}", "", "");
                Currency currency = result.currency;
                if (currency != null)
                {
                    List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
                    var countries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");
                    var countriesList = countries.countries;
                    foreach (var country in countriesList)
                    {
                        var selectItem = new SelectListItem() { Value = country.Id.ToString(), Text = country.NameEN };
                        countriesSelectedList.Add(selectItem);
                    }
                    ViewBag.SelectedList = countriesSelectedList;
                    return View(currency);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: AgentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Currency currency)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Currency>.CallApiPut($"Currencies/EditCurrency", currency, "");
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
                    countriesSelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = countriesSelectedList;
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AgentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await ApiCaller<GetCurrency, string>.CallApiGet($"Currencies/GetCurrencyById?Id={id}", "", "");
                Currency currency = result.currency;
                if (currency != null)
                {
                    return View(currency);

                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: AgentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Currencies/DeleteCurrency?id={id}", "", "");
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
    }
}
