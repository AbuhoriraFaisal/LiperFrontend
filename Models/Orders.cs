namespace LiperFrontend.Models
{
    public class Orders
    {
        public List<Order> orders { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GetOrder
    {
        public Order order { get; set; }
        public responseMessage responseMessage { get; set; }
    }
}
