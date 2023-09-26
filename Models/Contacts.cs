using System.ComponentModel.DataAnnotations;

namespace LiperFrontend.Models
{
    public class Contact
    {
        public int id { get; set; }
        [Required]
        public string email  { get; set; }
        [Required]
        public string phone { get; set; }
        public bool isActive { get; set; }
    }

    public class Contacts
    {
        public responseMessage responseMessage { get; set; }
        public List<Contact> contacts { get; set; }
    }
}
