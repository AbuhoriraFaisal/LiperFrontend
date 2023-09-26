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

        public async Task<IActionResult> CategorySubCategories(int Id)
        {
            var subcategoriesList = await ApiCaller<SubCategories, string>.CallApiGet($"SubCategories/GetSubCategoryByCategoryId?categoryId={Id}", "", "");
            return View(subcategoriesList.Item1.subCategories);
        }

        // GET: CategoryController/Create
        public ActionResult Create(int id)
        {
            ViewBag.categoryId = id;
            return View();
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
                    return RedirectToAction("CategorySubCategories", new { Id = subCategory.categoryId });
                }
                else
                {
                    ViewBag.categoryId = subCategory.categoryId;
                    return View();
                }
            }
            catch
            {
                ViewBag.categoryId = subCategory.categoryId;
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var subcategory = await ApiCaller<GetSubCategory, string>.CallApiGet($"SubCategories/GetSubCategoryById?Id={id}", "", "");
            SubCategory sCategory = subcategory.Item1.subCategory;
            if (sCategory != null)
            {
                return View(sCategory);
            }
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SubCategory subCategory)
        {
            var catId = subCategory.categoryId;
            try
            {
                var response = await ApiCaller<defaultResponse, SubCategory>.CallApiPut($"SubCategories/EditSubCategory", subCategory, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    return RedirectToAction("CategorySubCategories", new { Id = catId });
                }
                else
                {
                    return View();
                }
                return RedirectToAction("CategorySubCategories", new { Id = catId });
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var getsubCategory = await ApiCaller<GetSubCategory, string>.CallApiGet($"SubCategories/GetSubCategoryById?Id={id}", "", "");
            SubCategory subCategory = getsubCategory.Item1.subCategory;
            if (subCategory != null)
            {
                
                return View(subCategory);
            }
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, SubCategory collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"SubCategories/DeleteSubCategory?Id={id}", "", "");
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    return RedirectToAction("CategorySubCategories", new { Id = 1 });
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
