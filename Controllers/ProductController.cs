using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics.Metrics;
using System.IO;
using System.Security.Policy;

namespace LiperFrontend.Controllers
{
    public class ProductController : Controller
    {
        private readonly string contentRoot;

        public ProductController(IWebHostEnvironment env)
        {
            contentRoot = env.ContentRootPath;
        }

        // GET: ProductController
        public async Task<ActionResult> Index(int pg = 1)
        {
            try
            {
                Pager pager = new Pager();
                pager.CurrentPage = pg;
                var response = await ApiCaller<Products, string>.CallApiGet($"Products?page={pager.CurrentPage}&pageSize={pager.PageSize}", "", "");
                pager.CurrentPage = response.currentPage;
                pager.TotalPages = response.totalPages;
                pager.TotalItems = response.totalCount;
                this.ViewBag.Pager = pager;
                if (response.products is null)
                {
                    return View(new List<Product>());
                }
                return View(response.products);
            }
            catch (Exception ex)
            {
                return View(new List<Product>());
            }
        }

        // GET: ProductController/Details/5
        [HttpGet("Product/Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var product = await ApiCaller<GetProduct, string>.CallApiGet($"Products/GetById?Id={id}", "", "");

                if (product.getProductByIdResponseModel.product != null)
                {
                    return View(product.getProductByIdResponseModel.product);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        

        // GET: ProductController/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                List<SelectListItem> SelectedList = new List<SelectListItem>();
                var response = await ApiCaller<SubCategories, string>.CallApiGet("SubCategories", "", "");
                var cities = response.subCategories;
                foreach (var city in cities)
                {
                    var selectItem = new SelectListItem() { Value = city.id.ToString(), Text = city.nameEN };
                    SelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = SelectedList;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Product>.CallApiPostProduct($"Products", product, "");
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

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var product = await ApiCaller<GetProduct, string>.CallApiGet($"Products/GetById?Id={id}", "", "");

                if (product.getProductByIdResponseModel.product != null)
                {
                    product.getProductByIdResponseModel.product.productMainImage = ApiCaller<Product, Product>.Base_Url_files +
                        product.getProductByIdResponseModel.product.productMainImage;
                    HttpContext.Session.SetString("productMainImage",
                                                product.getProductByIdResponseModel.product.productMainImage);
                    List<SelectListItem> SelectedList = new List<SelectListItem>();
                    var response = await ApiCaller<SubCategories, string>.CallApiGet("SubCategories", "", "");
                    var cities = response.subCategories;
                    foreach (var city in cities)
                    {
                        var selectItem = new SelectListItem() { Value = city.id.ToString(), Text = city.nameEN };
                        if (selectItem.Value.Equals(product.getProductByIdResponseModel.product.subCategoryId))
                        {
                            selectItem.Selected = true;
                        }
                        SelectedList.Add(selectItem);
                    }
                    ViewBag.SelectedList = SelectedList;
                    return View(product.getProductByIdResponseModel.product);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product product)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Product>.CallApiPutProduct($"Products", product, "");
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var product = await ApiCaller<GetProduct, string>.CallApiGet($"Products/GetById?Id={id}", "", "");

                if (product.getProductByIdResponseModel.product != null)
                {
                    return View(product.getProductByIdResponseModel.product);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Products?id={id}", "", "");
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
