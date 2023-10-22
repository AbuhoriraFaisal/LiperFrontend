using LiperFrontend.Enums;
using LiperFrontend.Models;
using LiperFrontend.Services;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class ProductImageController : Controller
    {
        public async Task<ActionResult> Index(int Id)
        {
            try
            {
                var product = await ApiCaller<ProductImages, string>.CallApiGet($"ProductsImages/GetByProductId?productId={Id}", "", "");

                if (product.productsImages != null)
                {
                    foreach (var image in product.productsImages)
                    {
                        image.imageUrl = ApiCaller<Product, Product>.Base_Url_files + image.imageUrl;
                    }
                    return View(product.productsImages);
                }
                return View(new List<ProductImage>());
            }
            catch (Exception ex)
            {
                return View(new List<ProductImage>());
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeleteImage(int id, ProductImage image)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"ProductsImages?id={id}", "", "");

                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
                if (HttpContext.Session.Get("pId") is not null)
                {
                    image.productId = int.Parse(HttpContext.Session.GetString("pId"));
                }
                return View(image);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> DeleteImage(int id)
        {
            try
            {
                var product = await ApiCaller<GetProductImage, string>.CallApiGet($"ProductsImages/GetById?Id={id}", "", "");

                if (product.productsImage != null)
                {
                    product.productsImage.imageUrl = ApiCaller<Country, Country>.Base_Url_files +
                                                            product.productsImage.imageUrl;
                    HttpContext.Session.SetString("pId", product.productsImage.productId.ToString());
                    return View(product.productsImage);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public async Task<ActionResult> ChangeImage(int id)
        {
            try
            {
                var product = await ApiCaller<GetProductImage, string>.CallApiGet($"ProductsImages/GetById?Id={id}", "", "");

                if (product.productsImage != null)
                {
                    product.productsImage.imageUrl = ApiCaller<Country, Country>.Base_Url_files +
                                                            product.productsImage.imageUrl;
                    return View(product.productsImage);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeImage(int id, ProductImage image)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, ProductImage>.CallApiPutProductImage($"ProductsImages", image, "");
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);
                    var product = await ApiCaller<GetProductImage, string>.CallApiGet($"ProductsImages/GetById?Id={id}", "", "");

                    if (product.productsImage != null)
                    {
                        product.productsImage.imageUrl = ApiCaller<Country, Country>.Base_Url_files +
                                                                product.productsImage.imageUrl;
                        return View(product.productsImage);
                    }
                    return View(image);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        public async Task<IActionResult> Create(int Id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductImage image)
        {
            try
            {
                if (image.files is null)
                    return View();
                image.id = 0;
                var response = await ApiCaller<defaultResponse, ProductImage>.CallApiPostProductImage($"ProductsImages", image, "");
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, response.responseMessage.messageEN);

                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}
