using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class ImageSliderController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var slider = await ApiCaller<ImageSliders, string>.CallApiGet("SilderImages", "", "");
                if (slider.silders is not null)
                {
                    foreach (var item in slider.silders)
                    {
                        item.imageURL = ApiCaller<string,string>.Base_Url_files+item.imageURL;
                    }
                    return View(slider.silders);
                }
                return View(new List<ImageSlider>());
            }
            catch (Exception ex)
            {
                return View(new List<ImageSlider>());
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: SliderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ImageSlider slider)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, ImageSlider>.CallApiPostImageSliedr($"SilderImages", slider, "");

                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
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
                var result = await ApiCaller<GetImageSlider, string>.CallApiGet($"SilderImages/GetById?id={id}", "", "");
                ImageSlider slider = result.silder;
                if (slider != null)
                {
                    slider.imageURL = ApiCaller<string, string>.Base_Url_files + slider.imageURL;
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
        public async Task<ActionResult> Edit(int id, ImageSlider slider)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, ImageSlider>.CallApiPutImageSliedr($"SilderImages", slider, "");
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
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
                var result = await ApiCaller<GetImageSlider, string>.CallApiGet($"SilderImages/GetById?id={id}", "", "");
                ImageSlider slider = result.silder;
                if (slider != null)
                {
                    slider.imageURL = ApiCaller<string, string>.Base_Url_files + slider.imageURL;
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
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"SilderImages?id={id}", "", "");
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
