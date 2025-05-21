using System.ComponentModel.DataAnnotations;
namespace Pizza_Shop_.ViewModels;

public class AddSectionViewModel
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
