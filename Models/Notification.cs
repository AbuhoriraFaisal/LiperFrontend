using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LiperFrontend.Models
{
    public class Notification
    {
        public int id { get; set; } = 0;
        public string text { get; set; }
        public string description { get; set; }
        public IFormFile? files { get; set; }
        public bool isRead { get; set; } = false;
        public int userTypeId { get; set; }
        [DisplayName("Phone number")]
        public string? agent_customer_Phone { get; set; } // agent-customer-Id
    }
    public class Notifications
    {
        public List<Notification> agentNotifications { get; set; }
        public List<Notification> notifications { get; set; }
        public responseMessage responseMessage { get; set; }
        public int totalCount { get; set; }
        public int totalPages { get; set; }
        public int currentPage { get; set; }
    }
    public class GetNotification
    {
        public Notification agentNotifications { get; set; }
        public responseMessage responseMessage { get; set; }
    }
}
