using System.ComponentModel.DataAnnotations.Schema;
namespace Pizza_Shop_.ViewModels{
public class CreateEmailViewModel
{
    public int Id {get; set;}
    public string Email { get; set; } = string.Empty;
    public string TemporaryPassword { get; set; } = string.Empty;
}
}