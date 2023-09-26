using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class TermsAndConditionsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var conditions = await ApiCaller<Conditions, string>.CallApiGet("TermsAndConditions", "", "");

            return View(conditions.Item1.conditions);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Conditions conditions)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Conditions>.CallApiPost($"TermsAndConditions", conditions, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var condition = await ApiCaller<GetCondition, string>.CallApiGet($"TermsAndConditions/GetTermsAndConditions?Id={id}", "", "");
                Condition cond = condition.Item1.condition;
                if (cond != null)
                {
                    return View(cond);
                }
                else { 
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
            if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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
    }
}
