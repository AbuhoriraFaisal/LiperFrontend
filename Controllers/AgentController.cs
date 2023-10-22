using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiperFrontend.Controllers
{
    public class AgentController : Controller
    {
        // GET: AgentController
        public async Task<ActionResult> Index()
        {
            try
            {
                var agents = await ApiCaller<Agents, string>.CallApiGet("Agents", "", "");
                if (agents.agents is not null)
                {
                    return View(agents.agents);
                }
                return View(new List<Agent>());
            }
            catch (Exception ex)
            {
                return View(new List<Agent>());
            }
        }

        // GET: AgentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AgentController/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                List<SelectListItem> SelectedList = new List<SelectListItem>();
                var response = await ApiCaller<Cities, string>.CallApiGet("cities", "", "");
                var cities = response.cities;
                foreach (var city in cities)
                {
                    var selectItem = new SelectListItem() { Value = city.id.ToString(), Text = city.nameEN };
                    if (selectItem.Value == city.countryId.ToString())
                        selectItem.Selected = true;
                    SelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = SelectedList;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: AgentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Agent agent)
        {
            try
            {
                agent.password = "121";
                var response = await ApiCaller<defaultResponse, Agent>.CallApiPost($"Agents", agent, "");
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");

                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");

                }
                List<SelectListItem> SelectedList = new List<SelectListItem>();
                var cityResponse = await ApiCaller<Cities, string>.CallApiGet("cities", "", "");
                var cities = cityResponse.cities;
                foreach (var city in cities)
                {
                    var selectItem = new SelectListItem() { Value = city.id.ToString(), Text = city.nameEN };
                    if (selectItem.Value == city.countryId.ToString())
                        selectItem.Selected = true;
                    SelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = SelectedList;
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AgentController/Edit/5
        public async Task< ActionResult > Edit(int id)
        {
            try
            {
                var result = await ApiCaller<GetAgent, string>.CallApiGet($"Agents/GetById?Id={id}", "", "");
                Agent agent = result.agent;
                if (agent != null)
                {
                    List<SelectListItem> SelectedList = new List<SelectListItem>();
                    var cityResponse = await ApiCaller<Cities, string>.CallApiGet("cities", "", "");
                    var cities = cityResponse.cities;
                    foreach (var city in cities)
                    {
                        var selectItem = new SelectListItem() { Value = city.id.ToString(), Text = city.nameEN };
                        if (selectItem.Value == city.countryId.ToString())
                            selectItem.Selected = true;
                        SelectedList.Add(selectItem);
                    }
                    ViewBag.SelectedList = SelectedList;
                    return View(agent);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: AgentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Agent agent)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Agent>.CallApiPut($"Agents",agent , "");
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Operation Succeeded!");

                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Operation Failed !");

                }
                List<SelectListItem> SelectedList = new List<SelectListItem>();
                var cityResponse = await ApiCaller<Cities, string>.CallApiGet("cities", "", "");
                var cities = cityResponse.cities;
                foreach (var city in cities)
                {
                    var selectItem = new SelectListItem() { Value = city.id.ToString(), Text = city.nameEN };
                    if (selectItem.Value == city.countryId.ToString())
                        selectItem.Selected = true;
                    SelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = SelectedList;
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AgentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await ApiCaller<GetAgent, string>.CallApiGet($"Agents/GetById?Id={id}", "", "");
                Agent agent = result.agent;
                if (agent != null)
                {
                    return View(agent);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: AgentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Agents?id={id}", "", "");
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
                    return View();
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, response.responseMessage.messageEN);
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
