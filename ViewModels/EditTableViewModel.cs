using System.ComponentModel.DataAnnotations.Schema;
using Pizza_Shop_.Models;
namespace Pizza_Shop_.ViewModels
{
    public class EditTableViewModel
    {
        public string Table_number { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public TableStatus Status { get; set; }
        public int SectionId { get; set; }
        [Column("table_id")]
        public int Id { get; set; }
    }
}