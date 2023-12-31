﻿using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class TermsAndConditionsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var conditions = await ApiCaller<Conditions, string>.CallApiGet("TermsAndConditions", "", "");
                if (conditions.conditions is not null)
                {
                    return View(conditions.conditions);
                }
                return View(new List<Condition>());
            }
            catch (Exception ex)
            {
                return View(new List<Condition>());
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Condition condition)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Condition>.CallApiPost($"TermsAndConditions", condition, "");
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    return RedirectToAction(nameof(Index));
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
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var conditions = await ApiCaller<Conditions, string>.CallApiGet("TermsAndConditions", "", "");

                //var condition = await ApiCaller<GetCondition, string>.CallApiGet($"TermsAndConditions/GetTermsAndConditions?Id={id}", "", "");
                Condition cond = conditions.conditions.Where(s => s.id == id).FirstOrDefault();
                if (cond != null)
                {
                    return View(cond);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var conditions = await ApiCaller<Conditions, string>.CallApiGet("TermsAndConditions", "", "");

                //var condition = await ApiCaller<GetCondition, string>.CallApiGet($"TermsAndConditions/GetTermsAndConditions?Id={id}", "", "");
                Condition cond = conditions.conditions.Where(s => s.id == id).FirstOrDefault();
                if (cond != null)
                {
                    return View(cond);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        // POST: termsandcondetions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"TermsAndConditions?Id={id}", "", "");
            if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Condition condition)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Condition>.CallApiPut($"TermsAndConditions", condition, "");
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    return RedirectToAction("Index");
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

    }
}
