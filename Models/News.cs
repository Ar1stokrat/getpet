namespace PetAdoptionPlatform.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedByUserId { get; set; }

        public User CreatedByUser { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
