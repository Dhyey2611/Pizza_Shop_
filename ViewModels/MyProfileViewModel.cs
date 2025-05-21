using System.ComponentModel.DataAnnotations;

namespace Pizza_Shop_.ViewModels
{
    public class MyProfileViewModel
    {
        [Required]
        public string Name { get; set; } =string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string EmailId { get; set; } =string.Empty;
        public string Phone { get; set; } =string.Empty;
        public string Country { get; set; } =string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}