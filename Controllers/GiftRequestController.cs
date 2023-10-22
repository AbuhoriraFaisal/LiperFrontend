using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class GiftRequestController : Controller
    {
        // GET: GiftRequestController
        public async Task<ActionResult> Index(int pg = 1)
        {
            try
            {
                Pager pager = new Pager();
                pager.CurrentPage = pg;
                var response = await ApiCaller<GiftRequests, string>.CallApiGet($"GiftRequiests?page={pager.CurrentPage}&pageSize={pager.PageSize}", "", "");
                
                    pager.CurrentPage = response.currentPage;
                    pager.TotalPages = response.totalPages;
                    pager.TotalItems = response.totalCount;
                    this.ViewBag.Pager = pager;
                if (response.giftRequiests != null)
                {
                    return View(response.giftRequiests);
                }
                return View(new List<GiftRequest>());
            }
            catch (Exception ex)
            {
                return View(new List<GiftRequest>());
            }
        }

        // GET: GiftRequestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GiftRequestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GiftRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: GiftRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GiftRequestController/Edit/5
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

        // GET: GiftRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GiftRequestController/Delete/5
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
