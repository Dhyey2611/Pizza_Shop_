using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Pizza_Shop_.Models
{
    public class User
    {
        [Key]
        [Column("user_id")]
        public int user_id { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; } = string.Empty;

        // public int Age { get; set; }

       
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; } = string.Empty; // ✅ Add this
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [Column("Password")]  // <-- This is where you use the Column attribute
        public string? password{ get; set; }

        [Column("Password_hash")]  // ✅ Auto-generated, never used for login
        public string? password_hash { get; set; }
        public string user_type { get; set; } = "User";  // Optional: UserType based on your requirement, e.g., Admin or User.
        public string status { get; set; } = "Active"; // Optional: to track active or inactive users.
        public DateTime created_at { get; set; } = DateTime.UtcNow;

         // ✅ Newly Added Fields
        [Column("Phone_no")]
        public string? phone_no { get; set; }

        [Column("Country")]
        public string? country { get; set; }

        [Column("State")]
        public string? state { get; set; }

        [Column("City")]
        public string? city { get; set; }

        [Column("Address")]
        public string? address { get; set; }

        [Column("Zipcode")]
        public string? zip_code { get; set; }
        [NotMapped]
        public int country_id { get; set; }  // Add this
        [NotMapped]
        public int state_id { get; set; }    // Add this
        [NotMapped]
        public int city_id { get; set; }     // Add this
        [Required]
        [Column("Username")]
        public string Username { get; set; } = string.Empty;

    }
}
