using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiperFrontend.Controllers
{
    public class BankAccountController : Controller
    {

        public async Task<ActionResult> Index()
        {
            var response = await ApiCaller<BankAccounts, string>.CallApiGet("BankAccounts", "", "");
            return View(response.Item1.accounts);
        }


        // GET: AgentController/Create
        public async Task<ActionResult> Create()
        {
            List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
            var response = await ApiCaller<Banks, string>.CallApiGet("Banks", "", "");
            var banks = response.Item1.banks;
            foreach (var bank in banks)
            {
                var selectItem = new SelectListItem() { Value = bank.id.ToString(), Text = bank.name };
                countriesSelectedList.Add(selectItem);
            }
            ViewBag.SelectedList = countriesSelectedList;
            return View();
        }

        // POST: AgentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BankAccount account)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, BankAccount>.CallApiPost($"BankAccounts", account, "");
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
                var bankResponse = await ApiCaller<Banks, string>.CallApiGet("Banks", "", "");
                var banks = bankResponse.Item1.banks;
                foreach (var bank in banks)
                {
                    var selectItem = new SelectListItem() { Value = bank.id.ToString(), Text = bank.name };
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
            var result = await ApiCaller<GetBankAccount, string>.CallApiGet($"BankAccounts/GetById?Id={id}", "", "");
            BankAccount account = result.Item1.account;
            if (account != null)
            {
                List<SelectListItem> countriesSelectedList = new List<SelectListItem>();
                var response = await ApiCaller<Banks, string>.CallApiGet("Banks", "", "");
                var banks = response.Item1.banks;
                foreach (var b in banks)
                {
                    var selectItem = new SelectListItem() { Value = b.id.ToString(), Text = b.name };
                    countriesSelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = countriesSelectedList;
                return View(account);
            }
            return View();
        }

        // POST: AgentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BankAccount account)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, BankAccount>.CallApiPut($"BankAccounts", account, "");
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
                var bankResponse = await ApiCaller<Banks, string>.CallApiGet("Banks", "", "");
                var banks = bankResponse.Item1.banks;
                foreach (var b in banks)
                {
                    var selectItem = new SelectListItem() { Value = b.id.ToString(), Text = b.name };
                    countriesSelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = countriesSelectedList;
                return View(account);
            }
            catch
            {
                return View();
            }
        }

        // GET: AgentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await ApiCaller<GetBankAccount, string>.CallApiGet($"BankAccounts/GetById?Id={id}", "", "");
            BankAccount account = result.Item1.account;
            if (account != null)
            { 
                return View(account);
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
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"BankAccounts?id={id}", "", "");
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
