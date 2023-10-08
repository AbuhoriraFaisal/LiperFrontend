using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiperFrontend.Controllers
{
    public class BankController : Controller
    {

        public async Task<ActionResult> Index()
        {
            var response = await ApiCaller<Banks, string>.CallApiGet("Banks", "", "");
            return View(response.Item1.banks);
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
        public async Task<ActionResult> Create(Bank bank)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Bank>.CallApiPost($"Banks", bank, "");
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
        public async Task<ActionResult> Edit(int id)
        {
            var result = await ApiCaller<GetBank, string>.CallApiGet($"Banks/GetById?Id={id}", "", "");
            Bank bank = result.Item1.bank;
            if (bank != null)
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
                return View(bank);
            }
            return View();
        }

        // POST: AgentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Bank bank)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Bank>.CallApiPut($"Banks", bank, "");
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
                return View(bank);
            }
            catch
            {
                return View();
            }
        }

        // GET: AgentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await ApiCaller<GetBank, string>.CallApiGet($"Banks/GetById?Id={id}", "", "");
            Bank bank = result.Item1.bank;
            if (bank != null)
            {
               
                return View(bank);
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
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Banks?id={id}", "", "");
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
