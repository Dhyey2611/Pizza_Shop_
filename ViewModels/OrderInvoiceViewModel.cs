using System;
using System.Collections.Generic;
using Pizza_Shop_.Models;

namespace Pizza_Shop_.ViewModels
{
    public class OrderInvoiceViewModel
    {
        // Order-level info
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PaymentMode { get; set; } = string.Empty;

        // Table / Section info (optional static for now)
        public string Section { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;

        // Invoice items
        public required List<InvoiceDummyData> InvoiceItems { get; set; } = new List<InvoiceDummyData>();

        // Tax breakdown
        public decimal SubTotal { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal GST { get; set; }
        public decimal Other { get; set; }

        public decimal TotalAmountDue => SubTotal + CGST + SGST + GST + Other;
    }
}
