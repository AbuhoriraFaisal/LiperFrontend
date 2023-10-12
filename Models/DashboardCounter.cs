namespace LiperFrontend.Models
{
    public class DashboardCounter
    {
        public int allOrders { get; set; } = 0;
        public int newOrders { get; set; } = 0;
        public int cancelOrder {  get; set; }
        public int deleverdOrder {  get; set; }
        public int allCustomers { get; set; } = 0;
        public int allAgents {  get; set; }
        public int allFavStars {  get; set; }
        public int allProducts { get; set; } = 0;
    }
    public class GetDashboardCounter
    {
        public DashboardCounter dashboardCounter { get; set; }
        public responseMessage responseMessage { get; set; }
    }

}
