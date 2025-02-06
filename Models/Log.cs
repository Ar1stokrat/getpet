namespace PetAdoptionPlatform.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int PerformedByUserId { get; set; }
        public DateTime Timestamp { get; set; }

        public User PerformedByUser { get; set; }
    }
}
