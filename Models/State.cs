using System.ComponentModel;

namespace LiperFrontend.Models
{
    public class State
    {
        public int Id { get; set; }
        [DisplayName("State Name")]
        public string nameEN { get; set; }
        public string nameAR { get; set; }
        public int countryId { get; set; }
        public Country country { get; set; }
    }
}
