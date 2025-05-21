
using Pizza_Shop_.Models;

namespace Pizza_Shop_.ViewModels;

public class TableViewModel
{
    public string Table_number { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public TableStatus Status { get; set; }
    public int SectionId { get; set; }
}
