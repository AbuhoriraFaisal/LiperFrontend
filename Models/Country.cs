namespace LiperFrontend.Models
{
    public class Country
    {
        public int id { get; set; }
        public string nameEN { get; set; }
        public string nameAR { get; set; }
        public string flagImgUrl { get; set; }
        public int countrycode { get; set; }
        public IFormFile? flagImg { get; set; }
        public IFormFile File { get; set; }

    }
}