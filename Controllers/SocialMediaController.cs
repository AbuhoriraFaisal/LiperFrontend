using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class SocialMediaController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var response = await ApiCaller<SocialMedias, string>.CallApiGet("SocialMedias", "", "");
            if (response.Item1.socials != null)
            {
                foreach (var item in response.Item1.socials)
                {
                    item.ImageUrl = ApiCaller<string, string>.Base_Url_files + item.ImageUrl;
                }
                return View(response.Item1.socials);
            }
            return View(new List<OrderStatus>());

        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SocialMedia social)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, SocialMedia>.CallApiPostSocialMedia($"SocialMedias", social, "");
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
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");
                return View();
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await ApiCaller<GetSocialMedia, string>.CallApiGet($"SocialMedias/GetById?Id={id}", "", "");
                SocialMedia socialMedia = response.Item1.social;
                if (socialMedia != null)
                {
                    socialMedia.ImageUrl = ApiCaller<string, string>.Base_Url_files + socialMedia.ImageUrl;
                    return View(socialMedia);
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"SocialMedias?Id={id}", "", "");
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

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var response = await ApiCaller<GetSocialMedia, string>.CallApiGet($"SocialMedias/GetById?Id={id}", "", "");
                SocialMedia socialMedia = response.Item1.social;
                if (socialMedia != null)
                {
                    socialMedia.ImageUrl = ApiCaller<string, string>.Base_Url_files + socialMedia.ImageUrl;
                    return View(socialMedia);
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SocialMedia social)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, SocialMedia>.CallApiPutSocialMedia($"SocialMedias", social, "");
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
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "operation failed");

                return View(social);
            }
        }

    }
}
