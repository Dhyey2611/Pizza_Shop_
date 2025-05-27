
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_Shop_.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public int Order_Id { get; set; }
        [Column("order_date")]
        public DateTime Order_Date { get; set; }
        [Column("customer_name")]
        public string Customer_Name { get; set; } = string.Empty;
        [Column("status")]
        public string Status { get; set; } = "Pending";
        [Column("payment_mode")]
        public string Payment_Mode { get; set; } = string.Empty;
        [Column("rating")]
        public int Rating { get; set; }
        [Column("total_amount")]
        public decimal Total_Amount { get; set; }
        [Column("order_number")]
        public string Order_Number { get; set; } = string.Empty;
    }
}
