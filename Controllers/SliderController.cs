using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class SliderController : Controller
    {
        // GET: SliderController
        public async Task<ActionResult> Index()
        {
            try
            {
                var slider = await ApiCaller<Sliders, string>.CallApiGet("Sliders", "", "");
                return View(slider.Item1.sliders);
            }
            catch (Exception ex)
            {
                return View(new List<Slider>());
            }
        }

        // GET: SliderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SliderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SliderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Slider slider)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Slider>.CallApiPost($"Sliders/AddSlider", slider, "");

                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: SliderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var result = await ApiCaller<GetSlider, string>.CallApiGet($"Sliders/GetSlider?Id={id}", "", "");
                Slider slider = result.Item1.slider;
                if (slider != null)
                {
                    return View(slider);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: SliderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Slider slider)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Slider>.CallApiPut($"Sliders/EditSlider", slider, "");
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: SliderController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await ApiCaller<GetSlider, string>.CallApiGet($"Sliders/GetSlider?Id={id}", "", "");
                Slider slider = result.Item1.slider;
                if (slider != null)
                {
                    return View(slider);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: SliderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Sliders/DeleteSlider?id={id}", "", "");
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
