using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class SubCategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CategorySubCategories (int categoryId)
        {
            var subcategoriesList = await ApiCaller<SubCategories, string>.CallApiGet($"SubCategories/GetSubCategoryByCategoryId?categoryId={categoryId}", "", "");
            return View(subcategoriesList.Item1.subCategories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubCategory subCategory)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, SubCategory>.CallApiPost($"SubCategories", subCategory, "");
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
