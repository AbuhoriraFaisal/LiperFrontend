using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class GPremoveMyAccount : Controller
    {
       public IActionResult removeMyAccount()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult removeMyAccount(RemoveMyAccount removeMyAccount)
        {
            if(removeMyAccount.phone == null)
            {
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Warning, "Your phone number is Required!");

                return View();
            }
            else
            {
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Your account Removed Successfully!");
                return View();
            }
           
        }
    }
}
