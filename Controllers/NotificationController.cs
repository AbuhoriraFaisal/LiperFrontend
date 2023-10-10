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
    public class NotificationController : Controller
    {
        // GET: NotificationController
        public async Task<ActionResult> Index(int pg = 1)
        {

            Pager pager = new Pager();
            pager.CurrentPage = pg;
            var response = await ApiCaller<Notifications, string>.CallApiGet($"AgentNotifications?page={pager.CurrentPage}&pageSize={pager.PageSize}", "", "");
            pager.CurrentPage = response.Item1.currentPage;
            pager.TotalPages = response.Item1.totalPages;
            pager.TotalItems = response.Item1.totalCount;
            this.ViewBag.Pager = pager;
            return View(response.Item1.agentNotifications);
        }

       

        // GET: NotificationController/Create
        public ActionResult Create()
        {
            List<SelectListItem> SelectedList = new List<SelectListItem>();

            var selectItem = new SelectListItem() { Value = "1", Text = "Agent" };
            SelectedList.Add(selectItem);
            selectItem = new SelectListItem() { Value = "2", Text = "Client" };
            SelectedList.Add(selectItem);

            ViewBag.SelectedList = SelectedList;
            return View();
        }

        // POST: NotificationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Notification notification)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Notification>.CallApiPostAgentNotification($"AgentNotifications", notification, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                List<SelectListItem> SelectedList = new List<SelectListItem>();

                var selectItem = new SelectListItem() { Value = "1", Text = "Agent" };
                SelectedList.Add(selectItem);
                selectItem = new SelectListItem() { Value = "2", Text = "Client" };
                SelectedList.Add(selectItem);
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
