using LiperFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LiperFrontend.Shared;

namespace LiperFrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await ApiCaller<GetDashboardCounter, string>.CallApiGet("Dashboard/GetDashboardCounter", "", "");
                if (response.dashboardCounter is not null)
                {
                    return View(response.dashboardCounter);
                }
                return View(new DashboardCounter());
            }
            catch (Exception ex)
            {
                return View(new DashboardCounter());
            }
        }

        /// <summary>
        /// about us views 
        /// </summary>
        /// <returns></returns>
        /// 
        //[HttpGet]
        //public async Task<IActionResult> AboutUs()
        //{
        //    List<Aboutus> aboutUsList = await ApiCaller<Aboutus, Aboutus>.CallApiGetList("AboutUs", new Aboutus());
        //    return View(aboutUsList.FirstOrDefault());
        //}

        //[HttpPost]
        //public async Task<IActionResult> AboutUs(Aboutus aboutUsResponse , int id)
        //{
        //    var  aboutUsList = await ApiCaller<Aboutus, Aboutus>.CallApiPost("AboutUs",aboutUsResponse, "");
        //    var categoriesList = await ApiCaller<Categories, string>.CallApiGet("Categories" , "" ,"");
        //    return View(aboutUsList.Item1);
        //}

        /// <summary>
        /// get categories lis
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<IActionResult> Categories()
        {
            var categoriesList = await ApiCaller<Categories, string>.CallApiGet("Categories", "", "");
            return View(categoriesList.categories);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}