namespace LiperFrontend.Models
{
    public class GetPaymentMethods
    {
        public PaymentMethod paymentMethod { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GetBank
    {
        public Bank  bank { get; set; }
        public responseMessage responseMessage { get; set;}
    }
    public class Bank
    {
        public int id { get; set; }
        public int countryId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Country Country { get; set; }

    }
    public class Banks
    {
        public List<Bank> bsank { get; set; }
        public responseMessage responseMessage { get; set; }
    }
}