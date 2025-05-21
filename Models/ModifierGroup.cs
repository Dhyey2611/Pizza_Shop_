using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_Shop_.Models
{
    [Table("Modifier_Groups")]
    public class ModifierGroup
    {
        [Key]
        [Column("modifier_group_id")]
        public int ModifierGroupId { get; set; }

        [Column("modifier_group_name")]
        public string ModifierGroupName { get; set; } = string.Empty;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        [Column("description")]
        public string? Description { get; set; }
    }
}
