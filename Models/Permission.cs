using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_Shop_.Models
{
    [Table("Permissions")] // optional if table is named differently
    public class Permission
    {
        [Key]
        [Column("permission_id")]
        public int Id { get; set; }

        [Required]
        [Column("module")]
        public string Module { get; set; } = string.Empty;

        [Column("can_view")]
        public bool CanView { get; set; }

        [Column("can_edit")]
        public bool CanAddEdit { get; set; }

        [Column("can_delete")]
        public bool CanDelete { get; set; }

        [Column("role_id")] // assuming you renamed user_id to role_id
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;
    }
}
