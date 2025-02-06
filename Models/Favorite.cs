namespace PetAdoptionPlatform.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AnimalId { get; set; }

        public User User { get; set; }
        public Animal Animal { get; set; }
    }
}
