namespace LiperFrontend.Models
{
    public class Gender
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nameAR { get; set; }
    }
    public class Genders
    {
        public List<Gender> genders { get; set; }
        public responseMessage responseMessage { get; set; }
    }
}
