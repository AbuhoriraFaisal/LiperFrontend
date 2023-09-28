using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class AboutUsController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var abouts = await ApiCaller<GetAboutUs, string>.CallApiGet("AboutUs", "", "");
            return View(abouts.Item1.abouts);
        }
    }
}
