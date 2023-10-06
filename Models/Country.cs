using System.ComponentModel.DataAnnotations;

namespace LiperFrontend.Models
{
    public class Country
    {
        public int Id { get; set; } = 0;
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string flagImgUrl { get; set; }
        public int CountryCode { get; set; }
        //public IFormFile flagImg { get; set; }
        public IFormFile files { get; set; }

    }
}