namespace LiperFrontend.Models
{
    public class Customer
    {
        public int id { get; set; } = 0;
        public string name { get; set; }
        public string nameAR { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }

        public string email { get; set; }
        public string phone { get; set; }
        public Gender gender { get; set; }
        public int genderId { get; set; }
        public City city { get; set; }
        public int cityId { get; set; }
        public IFormFile files { get; set; }

    }
    public class Customers
    {
        public List<Customer> customers { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GetCustomer
    {
        public Customer customer { get; set; }
        public responseMessage responseMessage { get; set; }
    }
}
