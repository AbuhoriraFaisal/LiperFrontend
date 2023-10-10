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
            Pager pager = new Pager();
            pager.CurrentPage = pg;
            var response = await ApiCaller<Products, string>.CallApiGet($"Products?page={pager.CurrentPage}&pageSize={pager.PageSize}", "", "");
            pager.CurrentPage = response.Item1.currentPage;
            pager.TotalPages = response.Item1.totalPages;
            pager.TotalItems = response.Item1.totalCount;
            this.ViewBag.Pager = pager;
            return View(response.Item1.products);
        }

        // GET: ProductController/Details/5
        [HttpGet("Product/Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var product = await ApiCaller<GetProduct, string>.CallApiGet($"Products/GetById?Id={id}", "", "");

            if (product.Item1.getProductByIdResponseModel.product != null)
            {
                return View(product.Item1.getProductByIdResponseModel.product);
            }
            return View();
        }
        //public async Task<ActionResult> ProductImages(int id)
        //{
        //    var product = await ApiCaller<ProductImages, string>.CallApiGet($"ProductsImages", "", "");

        //    if (product.Item1.productsImages!= null)
        //    {
        //        return View(product.Item1.productsImages);
        //    }
        //    return View();
        //}

        // GET: ProductController/Create
        public async Task<ActionResult> Create()
        {
            List<SelectListItem> SelectedList = new List<SelectListItem>();
            var response = await ApiCaller<SubCategories, string>.CallApiGet("SubCategories", "", "");
            var cities = response.Item1.subCategories;
            foreach (var city in cities)
            {
                var selectItem = new SelectListItem() { Value = city.id.ToString(), Text = city.nameEN };
                SelectedList.Add(selectItem);
            }
            ViewBag.SelectedList = SelectedList;
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Product>.CallApiPostProduct($"Products", product, "");
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

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var product = await ApiCaller<GetProduct, string>.CallApiGet($"Products/GetById?Id={id}", "", "");

            if (product.Item1.getProductByIdResponseModel.product != null)
            {
                product.Item1.getProductByIdResponseModel.product.productMainImage = ApiCaller<Product, Product>.Base_Url_files +
                    product.Item1.getProductByIdResponseModel.product.productMainImage;
                HttpContext.Session.SetString("productMainImage",
                                            product.Item1.getProductByIdResponseModel.product.productMainImage);
                List<SelectListItem> SelectedList = new List<SelectListItem>();
                var response = await ApiCaller<SubCategories, string>.CallApiGet("SubCategories", "", "");
                var cities = response.Item1.subCategories;
                foreach (var city in cities)
                {
                    var selectItem = new SelectListItem() { Value = city.id.ToString(), Text = city.nameEN };
                    if (selectItem.Value.Equals(product.Item1.getProductByIdResponseModel.product.subCategoryId))
                    {
                        selectItem.Selected = true;
                    }
                    SelectedList.Add(selectItem);
                }
                ViewBag.SelectedList = SelectedList;
                return View(product.Item1.getProductByIdResponseModel.product);
            }
            return View();
        }

        public IActionResult ConvertToFormFile(string externalPath)
        {
            // Create a physical file provider for the external path
            var fileProvider = new PhysicalFileProvider(Path.GetDirectoryName(externalPath));

            // Get the file info from the external path
            var fileInfo = fileProvider.GetFileInfo(Path.GetFileName(externalPath));

            if (fileInfo.Exists)
            {
                // Open the file stream
                using (var stream = fileInfo.CreateReadStream())
                {
                    // Create a form file using the file stream
                    var formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(externalPath))
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "application/octet-stream" // Set the content type of the file
                    };

                    // Use the form file as needed
                    // ...
                }

                return Ok("External path converted to FormFile");
            }

            return NotFound();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product product)
        {
            try
            {
                if (product.files == null)
                {
                    //if (product.productMainImage == null)
                    //    product.productMainImage = HttpContext.Session.GetString("productMainImage");
                    //HttpContext.Session.Remove("productMainImage");
                    //string imgurl = contentRoot + "//temp.png";
                    //var wc = new System.Net.WebClient();
                    //wc.DownloadFile(product.productMainImage, imgurl);


                    //
                    var f = new FormFile(null, 0, 0, "", "");
                    product.files = f;

                    //    var fileProvider = new PhysicalFileProvider(Path.GetDirectoryName(imgurl));

                    //    if (System.IO.File.Exists(imgurl))
                    //    {
                    //        var fileinfo = new FileInfo(imgurl);
                    //        using (var stream = new FileStream(imgurl, FileMode.Open))
                    //        {
                    //            var formFile = new FormFile(stream, 0, stream.Length, null,fileinfo.Name)
                    //            {
                    //                Headers = new HeaderDictionary(),
                    //                ContentType = "png" // Set the content type of the file
                    //            };
                    //            formFile.Headers["Content-Disposition"] = $"form-data; name=\"files\"=\"{fileinfo.Name}\"";
                    //           //product.productMainImage = imgurl;
                    //            product.files = formFile;
                    //        }
                    //}
                    //}
                }
                var response = await ApiCaller<defaultResponse, Product>.CallApiPutProduct($"Products", product, "");
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
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
            var product = await ApiCaller<GetProduct, string>.CallApiGet($"Products/GetById?Id={id}", "", "");

            if (product.Item1.getProductByIdResponseModel.product != null)
            {
                return View(product.Item1.getProductByIdResponseModel.product);
            }
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Products?id={id}", "", "");
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.Item1.responseMessage.messageEN);
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
