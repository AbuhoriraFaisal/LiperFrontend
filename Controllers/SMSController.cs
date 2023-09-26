using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class SMSController : Controller
    {

        public IActionResult sendSMS()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> sendSMS(Sms sms)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var response = await ApiCaller<defaultResponse, Sms>.CallApiPost($"SMS.sendSMS", sms, "");
                    responseMessage responseMessage = response.Item1.responseMessage;
                    if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                    {
                        ViewBag.Result = responseMessage.messageEN;
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
