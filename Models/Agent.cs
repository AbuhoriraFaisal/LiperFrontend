namespace LiperFrontend.Models
{
    public class Agent
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public City city { get; set; }
        public int cityId { get; set; }

    }
    public class Agents
    {
        public List<Agent> agents { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GetAgent
    {
        public Agent agent { get; set; }
        public responseMessage responseMessage { get; set; }
    }
}
