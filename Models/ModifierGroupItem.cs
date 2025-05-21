using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_Shop_.Models
{
    [Table("Modifier_Group_Items")]
    public class ModifierGroupItem
    {
        [Key]
        [Column("modifier_item_id")]
        public int ModifierItemId { get; set; }

        [Column("modifier_group_id")]
        public int ModifierGroupId { get; set; }

        [Column("modifier_id")]
        public int ModifierId { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("unit")]
        public string Unit { get; set; } = string.Empty;
        public Modifier? Modifier { get; set; }
        public ModifierGroup? ModifierGroup { get; set; }
    }
}
