using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiperFrontend.Controllers
{
    public class UserController : Controller
    {
        public async Task< IActionResult> Index()
        {
			try
			{
                var response = await ApiCaller<Users, string>.CallApiGet("Users", "", "");
                if (response.users != null)
                {
                    return View(response.users);
                }
                return View(new List<Users>());
            }
			catch (Exception ex)
            {
                return View(new List<Users>());
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            
            try
            {
                var response = await ApiCaller<loginResponse, string>.CallApiGet($"Users/Login?userName={userLogin.userName}&passwprd={userLogin.passwprd}", "", "");
               loginResponse loginResponse = response;
                if (loginResponse is not null)
                {
                    if (loginResponse.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                    {
                        //ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");
                        HttpContext.Session.SetString("token", loginResponse.token);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "User  Failed to Login!");

                        return View();
                    }
                }
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "User  Failed to Login!");

                return View();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, User>.CallApiPost($"Users", user, "");
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

        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var response = await ApiCaller<GetUser, string>.CallApiGet($"Users/GetById?Id={id}", "", "");
                if (response.user is not null)
                {
                    return View(response.user);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, User>.CallApiPut($"Users", user, "");
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
                return View(user);
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await ApiCaller<GetUser, string>.CallApiGet($"Users/GetById?Id={id}", "", "");
                if (response.user is not null)
                {
                    return View(response.user);
                }
                return RedirectToAction("Index");
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
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Users?id={id}", "", "");
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
