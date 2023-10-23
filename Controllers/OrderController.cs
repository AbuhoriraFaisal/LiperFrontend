using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
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
                if (response.orders != null)
                {
                    List<SelectListItem> SelectedList = new List<SelectListItem>();
                    var orderStatus = await ApiCaller<OrderStatuses, string>.CallApiGet("OrderStatuses", "", "");
                    var statesList = orderStatus.orderStatuses;
                    foreach (var status in statesList)
                    {
                        var selectItem = new SelectListItem() { Value = status.id.ToString(), Text = status.name };
                        SelectedList.Add(selectItem);
                    }
                    ViewBag.SelectedList = SelectedList;

                    return View(response.orders.Where(s=>s.isPaid==true).ToList());
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
                if (response.order is null)
                    return RedirectToAction("Index");
                Order order = response.order;
                order.imageURL = ApiCaller<string, string>.Base_Url_files + order.imageURL;
                //get receiver data
                var receiverInfo = await ApiCaller<GetReceiverInfo, string>.CallApiGet($"Orders/GetReceiverInfo?orderId={Id}", "", "");
                if (receiverInfo.receiverInfo is not null)
                {
                    if (receiverInfo.receiverInfo.customer is not null)
                    {
                        if (receiverInfo.receiverInfo.customer.name.Contains("walking"))
                        {
                            order.ReceiverName = receiverInfo.receiverInfo.recipientName;
                            order.ReceiverPhone = receiverInfo.receiverInfo.recipientPhoneNumber;
                        }
                        else
                        {
                            order.ReceiverName = receiverInfo.receiverInfo.customer.name;
                            order.ReceiverPhone = receiverInfo.receiverInfo.customer.phone;
                        }
                    }
                    else
                    {
                        order.ReceiverName = "";
                        order.ReceiverPhone = "";
                    }
                    
                }
                return View(order);
            }
            catch (Exception ex)
            {
                return View(new Order());
            }
        }

        public async Task<IActionResult> UpdateStatus(int id)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiPut($"Orders/UpdateOrderStatuse?id={id}&orderStatuseId=4", "", "");
                return RedirectToAction("Details", new { Id = id });
            }
            catch
            {
                return RedirectToAction("Details", new { Id = id });
            }
            return View();
        }




    }
}
