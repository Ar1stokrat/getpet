namespace PetAdoptionPlatform.Models
{
    public class AdoptionRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AnimalId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected

        public User User { get; set; }
        public Animal Animal { get; set; }
    }
}
