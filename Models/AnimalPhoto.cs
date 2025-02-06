namespace PetAdoptionPlatform.Models
{
    public class AnimalPhoto
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public string PhotoUrl { get; set; }

        public Animal Animal { get; set; }
    }
}
