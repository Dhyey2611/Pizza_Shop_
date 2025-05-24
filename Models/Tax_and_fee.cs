using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_Shop_.Models
{
    [Table("Taxes_and_Fees")]
    public class Tax_and_fee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("type")]
        public string Type { get; set; } = string.Empty;

        [Required]
        [Column("value")]
        public decimal Value { get; set; }

        [Column("is_enabled")]
        public bool Is_enabled { get; set; }

        [Column("is_default")]
        public bool Is_default { get; set; } 
        [Column("Is_Active")]
        public bool Is_active { get; set; }
    }
}
