using FoodApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Entities
{
    public class User:BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime CreationAt { get; set; } = DateTime.Now;
        public DateTime? LastLogin { get; set; }
        public string? OtpCode { get; set; }
        public DateTime? OtpExpiry { get; set; }
        public ICollection<Recipe>? FavoriteRecipes { get; set; }
        public Role Role { get; set; }
    }
}
