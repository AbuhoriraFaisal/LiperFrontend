namespace LiperFrontend.Models
{
    public class Products
    {
        public List<Product> products { get; set; }
        public responseMessage responseMessage { get; set; }
        public int totalCount { get; set; }
        public int totalPages { get; set; }
        public int currentPage { get; set; }
    }
}
