using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Pizza_Shop_.Models
{
    public class Role
    {
        [Key]
        [Column("role_id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        // Optional: If you want to access all permissions related to a role
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}