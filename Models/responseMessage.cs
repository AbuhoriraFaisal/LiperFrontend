namespace LiperFrontend.Models
{
    public class responseMessage
    {
        public int statusCode { get; set; }
        public string messageEN { get; set; }
        public string messageAR { get; set; }

    }
    public class loginResponse 
    {
        public string token { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class defaultResponse
    {
        public responseMessage responseMessage { get; set; }
    }
    public class NotificationResponse : defaultResponse
    {
        public string imageUrl { get; set; }
    }
}
