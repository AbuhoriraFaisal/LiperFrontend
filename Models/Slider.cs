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
}
