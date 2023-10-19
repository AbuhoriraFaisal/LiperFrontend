namespace LiperFrontend.Models
{
    public class Users
    {
        public List<User> users { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GetUser
    {
        public User user { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string userName {  get; set; }
        public string Name {  get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool isActive { get; set; }
    }
    public class UserLogin
    {
        public string userName { get; set; }
        public string passwprd { get; set; }
    }
}
