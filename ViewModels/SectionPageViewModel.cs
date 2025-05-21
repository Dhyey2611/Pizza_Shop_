using Pizza_Shop_.Models;
namespace Pizza_Shop_.ViewModels;

public class SectionPageViewModel
{
    public List<Section> Sections { get; set; } = new List<Section>();
    public List<Table> Tables { get; set; } = new List<Table>();

    public int CurrentPage { get; set; }
    public int TotalItems { get; set; }
}
