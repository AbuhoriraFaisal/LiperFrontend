using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class PolicyController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var policies = await ApiCaller<Policies, string>.CallApiGet("Policies", "", "");
                if (policies.Item1.policies is not null)
                {
                    return View(policies.Item1.policies);
                }
                return View(new List<Policy>());
            }
            catch (Exception ex)
            {
                return View(new List<Policy>());
            }
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
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
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
                var response = await ApiCaller<GetPolicy, string>.CallApiGet($"Policies/GetById?Id={id}", "", "");
                Policy policy = response.Item1.policy;
                if (policy != null)
                {

                    return View(policy);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Policies?id={id}", "", "");
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var response = await ApiCaller<GetPolicy, string>.CallApiGet($"Policies/GetById?Id={id}", "", "");
                Policy policy = response.Item1.policy;
                if (policy != null)
                {

                    return View(policy);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Policy policy)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Policy>.CallApiPut($"Policies", policy, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                    return View();
                }
            }
            catch
            {
                return View(policy);
            }
        }

    }
}
