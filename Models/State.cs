using System.ComponentModel.DataAnnotations.Schema;
namespace Pizza_Shop_.Models
{
    public class State
    {
        [Column("id")]
        public int Id { get; set; } // Primary Key
        [Column("name")]
        public required string StateName { get; set; } // State Name
        [Column("code")]
        public required string CountryCode { get; set; } // Foreign Key (Country Code)
        [Column("country_id")]
        public int countryId { get; set; }
    }
}
