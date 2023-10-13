namespace LiperFrontend.Models
{
    public class OrderDetails
    {
        public int id { get; set; }
        public Product product { get; set; }
        public int quantity { get; set; }
        public int discount { get; set; }
        public decimal price { get; set; }
        public decimal totalPrice { get; set; }
        public int orderId { get; set; }
    }
}