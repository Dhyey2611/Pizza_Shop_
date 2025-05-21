using System.ComponentModel.DataAnnotations;

namespace Pizza_Shop_.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // [Required]
        // [MinLength(6)]
        // public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "User Type")]
        public string UserType { get; set; } = "User"; // Role

        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty; 

        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Active";
        public int CountryId {get; set;} 
        public int StateId  {get; set;} 
        public int CityId {get; set;} 
    }
}
