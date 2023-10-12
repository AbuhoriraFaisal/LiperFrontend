namespace LiperFrontend.Models
{
    public class FAQ
    {
        public int id { get; set; }
        public string text { get; set; }
        public string textAR { get; set; }
        public string description { get; set; }
        public string descriptionAR { get; set; }
        public string imageURL { get; set; }
        public IFormFile files { get; set; }
    }
    public class GetFAQ
    {
        public FAQ FAQ { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class FAQs
    {
        public List<FAQ> faqs { get; set; }
        public responseMessage responseMessage { get; set; }
    }
}

