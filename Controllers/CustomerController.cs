using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiperFrontend.Controllers
{
    public class CustomerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await ApiCaller<Customers, string>.CallApiGet("Customers", "", "");
               if (response.Item1.customers is not  null)
                {
                    return View(response.Item1.customers);
                }
                return View(new List<Customer>());
            }
            catch (Exception ex)
            {
                return View(new List<Customer>());
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await ApiCaller<GetCustomer, string>.CallApiGet($"Customers/GetById?id={id}", "", "");
                Customer customer = response.Item1.customer;
                if (customer != null)
                {
                    return View(customer);
                }
                return View();
            }
            catch (Exception rex)
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
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Customers?id={id}", "", "");
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                return View();
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Create()
        {
            try
            {
                List<SelectListItem> SelectedList = new List<SelectListItem>();
                var cityResponse = await ApiCaller<Cities, string>.CallApiGet("cities", "", "");
                var cities = cityResponse.Item1.cities;
                foreach (var city in cities)
                {
                    var selectItem = new SelectListItem() { Value = city.id.ToString(), Text = city.nameEN };
                    if (selectItem.Value == city.countryId.ToString())
                        selectItem.Selected = true;
                    SelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = SelectedList;

                List<SelectListItem> SelectedListgender = new List<SelectListItem>();
                var gendersresponse = await ApiCaller<Genders, string>.CallApiGet("Genders", "", "");
                var genders = gendersresponse.Item1.genders;
                foreach (var gender in genders)
                {
                    var selectItem = new SelectListItem() { Value = gender.id.ToString(), Text = gender.name };
                    SelectedListgender.Add(selectItem);
                }
                ViewBag.SelectedListgender = SelectedListgender;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] Customer customer)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Customer>.CallApiPostCustomer($"Customers/Registration", customer, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");

                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");

                }
                List<SelectListItem> SelectedList = new List<SelectListItem>();
                var cityResponse = await ApiCaller<Cities, string>.CallApiGet("cities", "", "");
                var cities = cityResponse.Item1.cities;
                foreach (var city in cities)
                {
                    var selectItem = new SelectListItem() { Value = city.id.ToString(), Text = city.nameEN };
                    if (selectItem.Value == city.countryId.ToString())
                        selectItem.Selected = true;
                    SelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = SelectedList;
                //
                List<SelectListItem> SelectedListgender = new List<SelectListItem>();
                var gendersresponse = await ApiCaller<Genders, string>.CallApiGet("Genders", "", "");
                var genders = gendersresponse.Item1.genders;
                foreach (var gender in genders)
                {
                    var selectItem = new SelectListItem() { Value = gender.id.ToString(), Text = gender.name };
                    SelectedListgender.Add(selectItem);
                }
                ViewBag.SelectedListgender = SelectedListgender;
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
