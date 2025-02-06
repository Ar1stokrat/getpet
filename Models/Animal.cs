using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PetAdoptionPlatform.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public int? Age { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ImageUrl { get; set; }
        public int? AddedByUserId { get; set; }

        public virtual User? AddedByUser { get; set; } // Важно для корректной загрузки пользователя
    }
}
