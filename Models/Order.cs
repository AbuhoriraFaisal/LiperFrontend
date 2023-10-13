namespace LiperFrontend.Models
{
    public class Order
    {
        public int id { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public int discount { get; set; }
        public bool isPercentage { get; set; }
        public decimal totalPrice { get; set; }
        public decimal price { get; set; }
        public int customerId { get; set; }
        public string imageURL { get; set; }
        public bool isPaid { get; set; }
        public int orderStatusId { get; set; }
        public int paymentMethodId { get; set; }
        public OrderStatus orderStatus { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public Customer customer { get; set; }
        public List<OrderDetails> orderDetails { get; set; }
        public int cityId { get; set; }
        public City city { get; set; }

    }
}
