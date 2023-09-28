namespace LiperFrontend.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nameAr { get; set; }
        public string description { get; set; }
        public string descriptionAr { get; set; }
        public decimal price { get; set; }
        public int discount { get; set; }
        public bool isPercentage { get; set; }
        public SubCategory SubCategory { get; set; }
        public int subCategoryId { get; set; }
        public IFormFile File { get; set; }
    }
}
