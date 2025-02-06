using Microsoft.AspNetCore.Identity;
using PetAdoptionPlatform.Models;

public class User : IdentityUser<int>
{
    public DateTime DateRegistered { get; set; } = DateTime.Now;

    public int RoleId { get; set; }  // Явное указание FK

    public Role Role { get; set; }

    public ICollection<Animal> Animals { get; set; }
    public ICollection<AdoptionRequest> AdoptionRequests { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Message> SentMessages { get; set; }
    public ICollection<Message> ReceivedMessages { get; set; }
    public ICollection<Favorite> Favorites { get; set; }
    public ICollection<Log> Logs { get; set; }
}
