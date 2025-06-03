using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_Shop_.Models
{
    [Table("Customer_History")]
    public class CustomerHistory
    {
        [Key]
        [Column("history_id")]
        public int HistoryId { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [Column("order_type")]
        public string OrderType { get; set; } = string.Empty;

        [Column("payment_status")]
        public string PaymentStatus { get; set; } = string.Empty;

        [Column("items_count")]
        public int ItemsCount { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }
        [Required]
        public  Customer Customer { get; set; } = default!;
    }
}
