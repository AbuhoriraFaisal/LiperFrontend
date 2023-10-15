using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace LiperFrontend.Controllers
{
    public class OrderController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                
                var response = await ApiCaller<Orders, string>.CallApiGet("Orders", "", "");
                if (response.Item1.orders != null)
                {
                    List<SelectListItem> SelectedList = new List<SelectListItem>();
                    var orderStatus = await ApiCaller<OrderStatuses, string>.CallApiGet("OrderStatuses", "", "");
                    var statesList = orderStatus.Item1.orderStatuses;
                    foreach (var status in statesList)
                    {
                        var selectItem = new SelectListItem() { Value = status.id.ToString(), Text = status.name };
                        SelectedList.Add(selectItem);
                    }
                    ViewBag.SelectedList = SelectedList;

                    return View(response.Item1.orders);
                }

                return View(new List<Order>());
            }
            catch (Exception ex)
            {
                return View(new List<Order>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            try
            {
                var response = await ApiCaller<GetOrder, string>.CallApiGet($"Orders/GetById?id={Id}", "", "");
                if (response.Item1.order is null)
                    return RedirectToAction("Indedx");
                Order order = response.Item1.order;
                order.imageURL = ApiCaller<string,string>.Base_Url_files + order.imageURL;
                return View(order);
            }
            catch (Exception ex)
            {
                return View(new Order());
            }
        }

        
       
    }
}
