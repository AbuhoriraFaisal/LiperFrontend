using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;

namespace LiperFrontend.Controllers
{
    //agent notifications
    public class NotificationController : Controller
    {
        // GET: NotificationController
        public async Task<ActionResult> Index(int pg = 1)
        {
            try
            {
                Pager pager = new Pager();
                pager.CurrentPage = pg;
                var response = await ApiCaller<Notifications, string>.CallApiGet($"AgentNotifications?page={pager.CurrentPage}&pageSize={pager.PageSize}", "", "");
                pager.CurrentPage = response.currentPage;
                pager.TotalPages = response.totalPages;
                pager.TotalItems = response.totalCount;
                this.ViewBag.Pager = pager;
                if (response.agentNotifications is not null)
                {
                    return View(response.agentNotifications);
                }
                return View(new List<Notification>());
            }
            catch (Exception ex)
            {
                return View(new List<Notification>());
            }

        }



        // GET: NotificationController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: NotificationController/Create
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
                    var response = await ApiCaller<NotificationResponse, Notification>.CallApiPostAgentNotification($"AgentNotifications/AddWithPhoneNumber", notification, "");
                    responseM = response.responseMessage;
                    if (responseM.statusCode.Equals(StatusCodes.Status200OK))
                    {
                        url = ApiCaller<NotificationResponse, Notification>.Base_Url_files + response.imageUrl;
                        responseM = await ApiCaller<responseMessage, Notification>.callSendAgentNotification(notification, url);
                    }
                }
                else
                {
                    var response = await ApiCaller<NotificationResponse, Notification>.CallApiPostAgentNotification($"AgentNotifications/AddMultiAgentNotifications", notification, "");
                    responseM = response.responseMessage;
                    if (responseM.statusCode.Equals(StatusCodes.Status200OK))
                    {
                        url = ApiCaller<NotificationResponse, Notification>.Base_Url_files + response.imageUrl;
                        responseM = await ApiCaller<responseMessage, Notification>.callSendAgentNotification(notification, url);
                    }
                }


                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, responseM.messageEN);
                List<SelectListItem> SelectedList = new List<SelectListItem>();


                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: NotificationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotificationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NotificationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotificationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
