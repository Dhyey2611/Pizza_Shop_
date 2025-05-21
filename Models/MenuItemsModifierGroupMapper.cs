using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_Shop_.Models
{
    [Table("Menu_Items_Modifier_group_Mapper_table")]
    public class MenuItemsModifierGroupMapper
    {
        [Key]
        [Column("item_id")]
        public int ItemId { get; set; }

        [Column("Mean")]
        public int Mean { get; set; }

        [Column("Max")]
        public int Max { get; set; }
        [Column("modifier_id")]
        public int ModifierId { get; set; }
    }
}
