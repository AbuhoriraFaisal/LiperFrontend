namespace LiperFrontend.Models
{
    public class Slider
    {
        public int id { get; set; }
        public string titleEN { get; set; }
        public string titleAR { get; set; }
        public bool isActive { get; set; }
    }
    public class Sliders
    {
        public List<Slider> sliders { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GetSlider
    {
        public Slider slider { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GetImageSlider
    {
        public ImageSlider silder { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class ImageSliders
    {
        public List<ImageSlider> silders { get; set; }
        public responseMessage responseMessage { get; set; }
    }

    public class ImageSlider
    {
        public int Id { get; set; }
        public string text { get; set; }
        public string textAR { get; set; }
        public string description { get; set; }
        public string descriptionAR { get; set; }
        public string imageURL { get; set; }
        public bool isActive { get; set; }
        public IFormFile? files { get; set; }
    }
}
