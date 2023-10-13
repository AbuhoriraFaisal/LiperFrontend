using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class OrderController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var response = await ApiCaller<Orders, string>.CallApiGet("Orders", "", "");
            if (response.Item1.orders != null)
            {
                return View(response.Item1.orders);
            }
            return View(new List<OrderStatus>());
        }

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
