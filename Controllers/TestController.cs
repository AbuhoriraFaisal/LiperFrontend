using LiperFrontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var tests =new List<Test>(){  
                new Test
            {
                name = "test",
                category= "Category1",
            },
             new Test
            {
                name = "test1",
                category= "Category2",
            }};
               
            return View(tests);
        }
    }
}
