using System.ComponentModel;

namespace LiperFrontend.Models
{
    public class City
    {
        public int? id { get; set; }
        [DisplayName("City")]
        public string? nameEN { get; set; }
        public string? nameAR { get; set; }
        public State? State { get; set; }
        public int? stateId { get; set; }
        public int? countryId { get; set; }
        public float longitude { get; set; }    
        public float latitude{ get; set; }

    }
}
