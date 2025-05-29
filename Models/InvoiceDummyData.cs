using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_Shop_.Models
{
    [Table("Invoice_Dummy_Data")]
    public class InvoiceDummyData
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public string ItemName { get; set; } = string.Empty;

        public string ModifierName { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
