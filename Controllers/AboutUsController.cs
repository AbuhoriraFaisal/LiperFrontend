using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class AboutUsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var abouts = await ApiCaller<AboutUss, string>.CallApiGet("AboutUs", "", "");
            return View(abouts.Item1.abouts);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AboutUs aboutUs)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, AboutUs>.CallApiPost($"AboutUs/AddAboutUs", aboutUs, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var abouts = await ApiCaller<AboutUss, string>.CallApiGet("AboutUs", "", "");
            if (abouts == null)
                return RedirectToAction("Index");

            AboutUs aboutUs = abouts.Item1.abouts.Where(s=>s.id==id).FirstOrDefault();
            if (aboutUs != null)
            {

                return View(aboutUs);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"?id={id}", "", "");
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> Edit(int id)
        {
            var abouts = await ApiCaller<AboutUss, string>.CallApiGet("AboutUs", "", "");
            if (abouts == null)
                return RedirectToAction("Index");

            AboutUs aboutUs = abouts.Item1.abouts.Where(s => s.id == id).FirstOrDefault();
            if (aboutUs != null)
            {

                return View(aboutUs);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AboutUs aboutUs)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, AboutUs>.CallApiPut($"AboutUs/EditAboutUs", aboutUs, "");
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
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
