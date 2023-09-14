using System.ComponentModel;

namespace LiperFrontend.Models
{
    public class AboutUsResponse
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
    }
}
