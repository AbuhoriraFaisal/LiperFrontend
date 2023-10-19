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
            try
            {
                var subcategoriesList = await ApiCaller<SubCategories, string>.CallApiGet($"SubCategories/GetSubCategoryByCategoryId?categoryId={Id}", "", "");
                if (subcategoriesList.Item1.subCategories is not null)
                {
                    return View(subcategoriesList.Item1.subCategories);
                }
                return View(new List<SubCategory>());
            }
            catch (Exception ex)
            {
                return View(new List<SubCategory>());
            }
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
                subCategory.id = 0;
                var response = await ApiCaller<defaultResponse, SubCategory>.CallApiPostSubCategory($"SubCategories", subCategory, "");
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
            try
            {
                var subcategory = await ApiCaller<GetSubCategory, string>.CallApiGet($"SubCategories/GetSubCategoryById?Id={id}", "", "");
                SubCategory sCategory = subcategory.Item1.subCategory;
                if (sCategory != null)
                {
                    sCategory.imageURL = ApiCaller<Country, Country>.Base_Url.Substring(0,
                        ApiCaller<Country, Country>.Base_Url.Length - 5) + sCategory.imageURL;

                    return View(sCategory);
                }
                return View();  
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SubCategory subCategory)
        {
            
            try
            {
                var catId = subCategory.categoryId;
                var response = await ApiCaller<defaultResponse, SubCategory>.CallApiPutSubCategory($"SubCategories/EditSubCategory", subCategory, "");
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
            try
            {
                var getsubCategory = await ApiCaller<GetSubCategory, string>.CallApiGet($"SubCategories/GetSubCategoryById?Id={id}", "", "");
                SubCategory subCategory = getsubCategory.Item1.subCategory;
                if (subCategory != null)
                {

                    return View(subCategory);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
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
