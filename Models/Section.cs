using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Pizza_Shop_.Models
{
    [Table("Sections")]
    public class Section
    {
        [Key]
        [Column("section_id")]
        public int SectionId { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("description")]
        public string? Description { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}