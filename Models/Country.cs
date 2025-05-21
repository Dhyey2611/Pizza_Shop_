using System.ComponentModel.DataAnnotations.Schema;
namespace Pizza_Shop_.Models
{
    public class Country
    {
        [Column("country_id")]
        public int Id { get; set; } // Primary Key
        [Column("country_name")]
        public required string CountryName { get; set; } // Country Name
        [Column("country_code")]
        public required string CountryCode { get; set; } // Country Code (e.g., "US", "IN")
    }
}
