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
    public class ReceiverInfo
    {
        public Customer customer { get; set; }
        public string recipientName { get; set; }
        public string recipientPhoneNumber { get; set; }
        public string note { get; set; }
    }
    public class GetReceiverInfo
    {
       public ReceiverInfo receiverInfo { get; set; }
        public responseMessage responseMessage { get; set;}
    }
}
