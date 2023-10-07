using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class PaymentMethodController : Controller
    {
        // GET: PaymentMethodController
        public async Task<ActionResult> Index()
        {
            var paymentMethod = await ApiCaller<PaymentMethods, string>.CallApiGet("PaymentMethods", "", "");
            return View(paymentMethod.Item1.paymentMethods);
        }

        // GET: PaymentMethodController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaymentMethodController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentMethod paymentMethod)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, PaymentMethod>.CallApiPost($"PaymentMethods", paymentMethod, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentMethodController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var getpaymentMethod = await ApiCaller<GetPaymentMethods, string>.CallApiGet($"PaymentMethods/GetById?id={id}", "", "");
            PaymentMethod pMethod = getpaymentMethod.Item1.paymentMethod;
            if (pMethod != null)
            {
                return View(pMethod);
            }
            return View();
        }

        // POST: PaymentMethodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PaymentMethod paymentMethod)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, PaymentMethod>.CallApiPut($"PaymentMethods", paymentMethod, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentMethodController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var getpaymentMethod = await ApiCaller<GetPaymentMethods, string>.CallApiGet($"PaymentMethods/GetById?id={id}", "", "");
            PaymentMethod pMethod = getpaymentMethod.Item1.paymentMethod;
            if (pMethod != null)
            {
                return View(pMethod);
            }
            return View();
        }

        // POST: PaymentMethodController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"PaymentMethods?id={id}", "", "");
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
