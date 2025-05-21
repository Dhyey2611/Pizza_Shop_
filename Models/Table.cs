using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Pizza_Shop_.Models
{
    [Table("Tables")]
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("table_id")]
        public int TableId { get; set; }
        [Column("table_number")]
        public string Table_number { get; set; } = string.Empty;
        [Column("capacity")]
        public int Capacity { get; set; }
        [Column("is_available")]
        public TableStatus Status { get; set; }
        [Column("section_id")]
        public int SectionId { get; set; }
        [Column("Is_Deleted")]
        public bool IsDeleted { get; set; }

    }
}