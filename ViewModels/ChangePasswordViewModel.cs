using System.ComponentModel.DataAnnotations;
namespace Pizza_Shop_.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Current Password is required.")]
        public string CurrentPassword { get; set; } =string.Empty;

        [Required(ErrorMessage = "New Password is required.")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm the new password.")]
        [Compare("NewPassword", ErrorMessage = "New Password and Confirm Password must match.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
