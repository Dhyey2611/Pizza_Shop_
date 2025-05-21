using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_Shop_.ViewModels
{
    public class EditSectionViewModel
    {
        [Column("section_id")]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}