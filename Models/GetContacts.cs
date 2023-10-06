namespace LiperFrontend.Models
{
    public class GetContacts
    {
        public Contact contact { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GetCountry
    {
        public Country country { get; set; }
        public responseMessage responseMessage { get; set; }
    }
}
