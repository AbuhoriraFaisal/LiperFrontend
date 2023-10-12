using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class OrderStatusController : Controller
    {
        // GET: OrderStatusController
        public async Task<ActionResult> Index()
        {
            var response = await ApiCaller<OrderStatuses, string>.CallApiGet("OrderStatuses", "", "");
            if (response.Item1.orderStatuses != null)
            {
                return View(response.Item1.orderStatuses);
            }
            return View(new List<OrderStatus>());

        }

        

        // GET: OrderStatusController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderStatusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderStatus status)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, OrderStatus>.CallApiPost($"OrderStatuses", status, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");

                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderStatusController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await ApiCaller<OrderStatuses, string>.CallApiGet("OrderStatuses", "", "");
            if (response.Item1.orderStatuses != null)
            {
                return View(response.Item1.orderStatuses.Where(s => s.id == id).FirstOrDefault());
            }
            return RedirectToAction("Index");
        }

        // POST: OrderStatusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  Edit(int id, OrderStatus status)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, OrderStatus>.CallApiPut($"OrderStatuses", status, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");

                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderStatusController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await ApiCaller<OrderStatuses, string>.CallApiGet("OrderStatuses", "", "");
            if (response.Item1.orderStatuses != null)
            {
                return View(response.Item1.orderStatuses.Where(s => s.id == id).FirstOrDefault());
            }
            return RedirectToAction("Index");
        }

        // POST: OrderStatusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"OrderStatuses?id={id}", "", "");
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
