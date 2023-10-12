using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace LiperFrontend.Controllers
{
    public class FAQsController : Controller
    {
        // GET: FAQsController
        public async Task<ActionResult> Index()
        {
            var response = await ApiCaller<FAQs, string>.CallApiGet("FAQs", "", "");
            if (response.Item1.faqs != null)
            {
                foreach (var item in response.Item1.faqs)
                {
                    item.imageURL = ApiCaller<FAQs, string>.Base_Url_files + item.imageURL;
                }
                return View(response.Item1.faqs);
            }
            return View(new List<FAQ>());
        }

       
        // GET: FAQsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FAQsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FAQ fAQ)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, FAQ>.CallApiPostFaqs($"FAQs", fAQ, "");
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

        // GET: FAQsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var contact_result = await ApiCaller<GetFAQ, string>.CallApiGet($"FAQs/GetById?Id={id}", "", "");
                FAQ faq = contact_result.Item1.FAQ;
                if (faq != null)
                {
                    faq.imageURL = ApiCaller<Country, Country>.Base_Url_files + faq.imageURL;
                    return View(faq);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: FAQsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(int id, FAQ faq)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, FAQ>.CallApiPutFaqs($"FAQs", faq, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");
                    return View(faq);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: FAQsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var contact_result = await ApiCaller<GetFAQ, string>.CallApiGet($"FAQs/GetById?Id={id}", "", "");
                FAQ faq = contact_result.Item1.FAQ;
                if (faq != null)
                {
                    faq.imageURL = ApiCaller<Country, Country>.Base_Url_files + faq.imageURL;
                    return View(faq);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: FAQsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"FAQs?id={id}", "", "");
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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
    }
}
