namespace LiperFrontend.Models
{
    public class Categories
    {
        public responseMessage responseMessage { get; set; }
        public  List<Category> categories { get; set; }
    }
    public class SubCategories
    {
        public List<SubCategory> subCategories { get; set; }
        
        public responseMessage responseMessage { get; set; }
    }
    public class SubCategory
    {
        public int id { get; set; }
        public int? categoryId { get; set; }
        public string nameEN { get; set; }
        public string nameAR { get; set; }
        public Category? category { get; set; }
    }
    public class GetCategory
    {
        public Category category { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class Category
    {
        public int id { get; set; }
        public string nameEN { get; set; }
        public string nameAR { get; set; }
        public string imageURL { get; set; }
    }
}
