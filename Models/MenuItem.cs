using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Pizza_Shop_.Models
{
    [Table("Menu_Items")]
    public class MenuItem
    {
        [Key]
        [Column("menu_item_id")]
        public int MenuItemId { get; set; } 

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("is_available")]
        public bool IsAvailable { get; set; }

        [Column("item_type")]
        public string? ItemType { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("unit")]
        public string? Unit { get; set; }

        [Column("tax_percentage")]
        public decimal? TaxPercentage { get; set; }

        [Column("default_tax")]
        public bool DefaultTax { get; set; }

        [Column("short_code")]
        public string? ShortCode { get; set; }
        
        [Column("rate")]
        public int? Rate { get;  set; }
        [NotMapped]
        public List<ModifierGroupItem>? SelectedModifiers { get; set; }
}
}