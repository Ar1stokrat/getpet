using Microsoft.AspNetCore.Identity;

namespace PetAdoptionPlatform.Models
{
    public class Role : IdentityRole<int> 
    {
        public Role() : base() { }

        public Role(string roleName) : base(roleName) { }

        
    }
}
