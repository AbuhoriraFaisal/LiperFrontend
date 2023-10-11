using System.ComponentModel;

namespace LiperFrontend.Models
{
    public class AboutUs
    {
        public int id { get; set; }
        [DisplayName("English Title ")]
        public string titleEN { get; set; }
        [DisplayName("Arabic Title ")]
        public string titleAR { get; set; }
        [DisplayName("English Description ")]
        public string descriptionEN { get; set; }
        [DisplayName("Arabic Description ")]
        public string descriptionAR { get; set; }
        public bool isActive { get; set; }
    }
    public class GetAboutUs
    {
        public AboutUs abouts { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class AboutUss
    {
        public List<AboutUs> abouts { get; set; }
        public responseMessage responseMessage { get; set; }
    }

}
