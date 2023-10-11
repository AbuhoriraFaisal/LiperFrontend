using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiperFrontend.Controllers
{
    public class CustomerNotificationController : Controller
    {
        public async Task<IActionResult> Index(int pg = 1)
        {
            try
            {
                Pager pager = new Pager();
                pager.CurrentPage = pg;
                var response = await ApiCaller<Notifications, string>.CallApiGet($"Notifications?page={pager.CurrentPage}&pageSize={pager.PageSize}", "", "");
                if (response != null)
                {
                    pager.CurrentPage = response.Item1.currentPage;
                    pager.TotalPages = response.Item1.totalPages;
                    pager.TotalItems = response.Item1.totalCount;
                    this.ViewBag.Pager = pager;
                    return View(response.Item1.notifications);
                }
                return View(new List<Notification>());
            }
            catch (Exception ex)
            {
                return View(new List<Notification>());
            }
        }

        //send notification
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Notification notification)
        {
            try
            {
                responseMessage responseM;
                string url = "";
                if (notification.agent_customer_Phone is not null)
                {
                    var response = await ApiCaller<NotificationResponse, Notification>.CallApiPostCustNotification($"Notifications/AddWithPhoneNumber", notification, "");
                    responseM = response.Item1.responseMessage;

                    if (responseM.statusCode.Equals(StatusCodes.Status200OK))
                    {
                        url = ApiCaller<NotificationResponse, Notification>.Base_Url_files + response.Item1.imageUrl;

                        responseM = await ApiCaller<responseMessage, Notification>.callSendCustNotification(notification, url);
                    }
                }
                else
                {
                    var response = await ApiCaller<NotificationResponse, Notification>.CallApiPostCustNotification($"Notifications/AddMultiNotofocations", notification, "");
                    responseM = response.Item1.responseMessage;
                    if (responseM.statusCode.Equals(StatusCodes.Status200OK))
                    {
                        url = ApiCaller<NotificationResponse, Notification>.Base_Url_files + response.Item1.imageUrl;

                        responseM = await ApiCaller<responseMessage, Notification>.callSendCustNotification(notification, url);
                    }
                }

                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, responseM.messageEN);
                return View();
            }
            catch
            {
                return View();
            }
        }

    }
}
