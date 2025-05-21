using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_Shop_.Models
{
    [Table("Modifiers")]
    public class Modifier
    {
        [Key]
        [Column("modifier_id")]
        public int ModifierId { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("price")]
        public decimal Price { get; set; }
    }
}