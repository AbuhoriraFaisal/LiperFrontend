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
            try
            {
                var response = await ApiCaller<OrderStatuses, string>.CallApiGet("OrderStatuses", "", "");
                if (response.orderStatuses != null)
                {
                    return View(response.orderStatuses);
                }
                return View(new List<OrderStatus>());
            }
            catch (Exception ex)
            {
                return View(new List<OrderStatus>());
            }

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
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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
            try
            {
                var response = await ApiCaller<OrderStatuses, string>.CallApiGet("OrderStatuses", "", "");
                if (response.orderStatuses != null)
                {
                    return View(response.orderStatuses.Where(s => s.id == id).FirstOrDefault());
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        // POST: OrderStatusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  Edit(int id, OrderStatus status)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, OrderStatus>.CallApiPut($"OrderStatuses", status, "");
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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
            try
            {
                var response = await ApiCaller<OrderStatuses, string>.CallApiGet("OrderStatuses", "", "");
                if (response.orderStatuses != null)
                {
                    return View(response.orderStatuses.Where(s => s.id == id).FirstOrDefault());
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        // POST: OrderStatusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"OrderStatuses?id={id}", "", "");
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
