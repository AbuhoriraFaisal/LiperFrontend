using System.ComponentModel.DataAnnotations;

namespace LiperFrontend.Models
{
    public class Sms
    {
        [Required]
        [Display(Name = "Phone Number")]
        public string mobileNumber { get; set; }
        [Required]
        [Display(Name = "Message")]
        public string body { get; set; }
    }
}
