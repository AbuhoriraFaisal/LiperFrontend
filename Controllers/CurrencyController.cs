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
            var currencies = await ApiCaller<Currencies, string>.CallApiGet("Currencies", "", "");
            return View(currencies.Item1.currencies);
        }


        // GET: AgentController/Create
        public async Task<ActionResult> Create()
        {
            List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
            var countries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");
            var countriesList = countries.Item1.countries;
            foreach (var country in countriesList)
            {
                var selectItem = new SelectListItem() { Value = country.Id.ToString(), Text = country.NameEN };
                countriesSelectedList.Add(selectItem);
            }
            ViewBag.SelectedList = countriesSelectedList;
            return View();
        }

        // POST: AgentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Currency currency)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Currency>.CallApiPost($"Currencies/AddCurrency", currency, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");

                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");

                }
                List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
                var countries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");
                var countriesList = countries.Item1.countries;
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
        public async Task< ActionResult > Edit(int id)
        {
            var result = await ApiCaller<GetAgent, string>.CallApiGet($"Currencies/GetById?Id={id}", "", "");
            Agent agent = result.Item1.agent;
            if (agent != null)
            {
                List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
                var countries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");
                var countriesList = countries.Item1.countries;
                foreach (var country in countriesList)
                {
                    var selectItem = new SelectListItem() { Value = country.Id.ToString(), Text = country.NameEN };
                    countriesSelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = countriesSelectedList;
               
            }
            return View();
        }

        // POST: AgentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Currency currency)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Currency>.CallApiPut($"Currencies",currency , "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");

                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");

                }
                List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
                var countries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");
                var countriesList = countries.Item1.countries;
                foreach (var country in countriesList)
                {
                    var selectItem = new SelectListItem() { Value = country.Id.ToString(), Text = country.NameEN };
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

        // GET: AgentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await ApiCaller<GetAgent, string>.CallApiGet($"Currencies/GetById?Id={id}", "", "");
            Agent agent = result.Item1.agent;
            if (agent != null)
            {
                List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
                var countries = await ApiCaller<Countries, string>.CallApiGet("Countries", "", "");
                var countriesList = countries.Item1.countries;
                foreach (var country in countriesList)
                {
                    var selectItem = new SelectListItem() { Value = country.Id.ToString(), Text = country.NameEN };
                    countriesSelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = countriesSelectedList;

            }
            return View();
        }

        // POST: AgentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Currencies?id={id}", "", "");
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
    }
}
