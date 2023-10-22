using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.Metrics;

namespace LiperFrontend.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            try
            {
                var categoriesList = await ApiCaller<Categories, string>.CallApiGet("Categories", "", "");
                if (categoriesList.categories is null)
                {
                    return View(new List<Category>());
                }
                foreach (var item in categoriesList.categories)
                {
                    item.imageURL = ApiCaller<Category, Category>.Base_Url_files + item.imageURL;
                }
                return View(categoriesList.categories);
            }
            catch (Exception ex)
            {
                return View(new List<Categories>());
            }
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Category>.CallApiPostCategory($"Categories/AddCategory", category, "");
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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

        // GET: CategoryController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var getcategory = await ApiCaller<GetCategory, string>.CallApiGet($"Categories/GetCategoryById?Id={id}", "", "");
                Category category = getcategory.category;
                if (category != null)
                {
                    category.imageURL = ApiCaller<Country, Country>.Base_Url.Substring(0,
                        ApiCaller<Country, Country>.Base_Url.Length - 5) + category.imageURL;
                    return View(category);
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
        public async Task<ActionResult> Edit(int id, Category category)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Category>.CallApiPutCategory($"Categories/EditCategory", category, "");
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var getcategory = await ApiCaller<GetCategory, string>.CallApiGet($"Categories/GetCategoryById?Id={id}", "", "");
                Category category = getcategory.category;
                if (category != null)
                {
                    return View(category);
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
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Categories/DeleteCategory?Id={id}", "", "");
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
