namespace LiperFrontend.Models
{
    public class SubCategory
    {
        public int id { get; set; }
        public int? categoryId { get; set; }
        public string nameEN { get; set; }
        public string nameAR { get; set; }
        public Category? category { get; set; }
    }
}
