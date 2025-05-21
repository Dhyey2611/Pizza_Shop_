using System.ComponentModel.DataAnnotations.Schema;
namespace Pizza_Shop_.Models
{
    public class City
    {
        [Column("id")]
        public int Id { get; set; } // Primary Key
        [Column("name")]
        public required string CityName { get; set; } // City Name
        [NotMapped]
        public required string StateName { get; set; } // Foreign Key (State Name)
        [Column("state_id")]
        public int StateId {get; set;}
    }
}
