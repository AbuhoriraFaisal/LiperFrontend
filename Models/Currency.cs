namespace LiperFrontend.Models
{
    public class Currency
    {
        public int id { get; set; }
        public string nameEN { get; set; }
        public string nameAR { get; set; }
        public string currencyCode { get; set; }
        public bool isActive { get; set; }
        public Country country { get; set; }
        public int countryId { get; set; }
    }
    public class Currencies
    {
        public List<Currency> currencies { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GetCurrency
    {
        public Currency currency { get; set; }
        public responseMessage responseMessage { get; set; }
    }
}
