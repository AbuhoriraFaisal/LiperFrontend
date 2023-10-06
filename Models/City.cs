namespace LiperFrontend.Models
{
    public class City
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public State? State { get; set; }
        public int? stateId { get; set; }
        public float longitude { get; set; }    
        public float latitude{ get; set; }

    }
}
