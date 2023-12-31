﻿using LiperFrontend.Enums;
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
            try
            {
                var abouts = await ApiCaller<AboutUss, string>.CallApiGet("AboutUs", "", "");
                if (abouts.abouts is not null)
                {
                    return View(abouts.abouts);
                }
                return View(new List<AboutUs>());
            }
            catch (Exception ex)
            {
                return View(new List<AboutUs>());
            }
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
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
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
            try
            {
                var abouts = await ApiCaller<AboutUss, string>.CallApiGet("AboutUs", "", "");
                if (abouts.abouts.Count ==0)
                {
                    return RedirectToAction("Index");
                }
                AboutUs aboutUs = abouts.abouts.Where(s => s.id == id).FirstOrDefault();
                if (aboutUs != null)
                {

                    return View(aboutUs);
                }
                return View();
            }
            catch (Exception ex)
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
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"?id={id}", "", "");
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
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
            try
            {
                var abouts = await ApiCaller<AboutUss, string>.CallApiGet("AboutUs", "", "");
                if (abouts == null)
                    return RedirectToAction("Index");

                AboutUs aboutUs = abouts.abouts.Where(s => s.id == id).FirstOrDefault();
                if (aboutUs != null)
                {

                    return View(aboutUs);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AboutUs aboutUs)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, AboutUs>.CallApiPut($"AboutUs/EditAboutUs", aboutUs, "");
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
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
