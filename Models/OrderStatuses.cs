namespace LiperFrontend.Models
{
    public class OrderStatuses
    {
        public List<OrderStatus> orderStatuses { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GetOrderStatuses
    {
        public OrderStatus orderStatuses { get; set; }
        public responseMessage responseMessage { get; set; }
    }

}
