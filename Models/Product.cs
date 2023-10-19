using LiperFrontend.Shared;

namespace LiperFrontend.Models
{
    public class Product
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string NameAR { get; set; }
        public string Description { get; set; }
        public string DescriptionAR { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public bool isPercentage { get; set; }
        public SubCategory SubCategory { get; set; }
        public int subCategoryId { get; set; }
        public IFormFile? files { get; set; }
        public string productMainImage { get; set; }


    }

    public class GetProduct
    {
        public responseMessage responseMessage { get; set; }
        public getProductByIdResponseModel getProductByIdResponseModel { get; set; }
    }
    public class getProductByIdResponseModel
    {
        public Product product { get; set; }
        public List<string> productsImages { get; set; }

    }

    public class ProductImages
    {
        public responseMessage responseMessage { get; set; }
        public List<ProductImage> productsImages { get; set; }
    }
    public class ProductImage
    {
        public int id { get; set; }
        public string imageUrl { get; set; }
        public Product product { get; set; }
        public int productId { get; set; }
        public IFormFile files { get; set; }
    }

    public class GetProductImage
    {
        public responseMessage responseMessage { get; set; }
        public ProductImage productsImage { get; set; }
    }
}
