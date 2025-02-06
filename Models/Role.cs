using Microsoft.AspNetCore.Identity;

namespace PetAdoptionPlatform.Models
{
    public class Role : IdentityRole<int> // Указываем, что ключ типа int
    {
        public Role() : base() { }

        public Role(string roleName) : base(roleName) { }

        // Дополнительные поля, если нужны
        // public string Description { get; set; }
    }
}
