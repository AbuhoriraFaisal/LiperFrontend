namespace LiperFrontend.Models
{
    public class Countries
    {
        public List<Country> countries { get; set; }
        public responseMessage responseMessage { get; set;}
        public int totalCount { get; set; }
        public int totalPages { get; set; }
        public int currentPage { get; set; }
    }
}