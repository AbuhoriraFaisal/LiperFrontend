using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class PolicyController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var policies = await ApiCaller<Policies, string>.CallApiGet("Policies", "", "");
            return View(policies.Item1.policies);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Policy policy)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Policy>.CallApiPost($"Policies", policy, "");
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
    }
}
