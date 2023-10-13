namespace LiperFrontend.Models
{
    public class SocialMedia
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public IFormFile files { get; set; }
        public string ImageUrl { get; set; }
    }
    public class SocialMedias
    {
        public List<SocialMedia> socials { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class GetSocialMedia
    {
        public SocialMedia social { get; set; }
        public responseMessage responseMessage { get; set; }
    }
}
