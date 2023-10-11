namespace LiperFrontend.Models
{
    public class Category
    {
        public int id { get; set; }
        public string nameEN { get; set; }
        public string nameAR { get; set; }
        public string imageURL { get; set; }
        public IFormFile files { get; set; }
    }
}
