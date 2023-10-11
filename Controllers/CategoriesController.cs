using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LiperFrontend.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            var categoriesList = await ApiCaller<Categories, string>.CallApiGet("Categories", "", "");
            return View(categoriesList.Item1.categories);
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

        // GET: CategoryController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var getcategory = await ApiCaller<GetCategory, string>.CallApiGet($"Categories/GetCategoryById?Id={id}", "", "");
            Category category = getcategory.Item1.category;
            if (category != null)
            {
                return View(category);
            }
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category category)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Category>.CallApiPut($"Categories/EditCategory", category, "");
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

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var getcategory = await ApiCaller<GetCategory, string>.CallApiGet($"Categories/GetCategoryById?Id={ id}", "", "");
            Category category = getcategory.Item1.category;
            if (category != null)
            {
                return View(category);
            }
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Categories/DeleteCategory?Id={id}", "", "");
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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
