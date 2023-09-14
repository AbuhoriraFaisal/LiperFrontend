namespace LiperFrontend.Models
{
    public class responseMessage
    {
        public int statusCode { get; set; }
        public string messageEN { get; set; }
        public string messageAR { get; set; }

    }

    public class defaultResponse
    {
        public responseMessage responseMessage { get; set; }
    }
}
