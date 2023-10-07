using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class CustomerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await ApiCaller<Customers, string>.CallApiGet("Customers", "", "");
            return View(response.Item1.customers);
        }
        public async Task<ActionResult> Delete(int id)
        {
            var response = await ApiCaller<GetCustomer, string>.CallApiGet($"Customers/GetById?id={id}", "", "");
            Customer customer = response.Item1.customer;
            if (customer != null)
            {
                return View(customer);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Customers?id={id}", "", "");
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
