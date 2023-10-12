namespace LiperFrontend.Models
{
    public class GiftRequest
    {
        public int id {  get; set; }
        public string text { get; set; }
        public int customerId { get; set; }
        public Customer customer { get; set; }
        public bool isRead  { get; set; }
    }
    public class GetGiftRequest
    {
        public GiftRequest giftRequest { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GiftRequests
    {
        public List<GiftRequest> giftRequiests { get; set; }
        public responseMessage responseMessage { get; set;}
        public int totalCount { get; set; }
        public int totalPages { get; set; }
        public int currentPage { get; set; }
    }
}
