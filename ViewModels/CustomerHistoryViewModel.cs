using System;
using System.Collections.Generic;

namespace Pizza_Shop_.ViewModels
{
    public class CustomerHistoryViewModel
    {
        // --- Top Section of Modal ---
        public string Name { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public DateTime ComingSince { get; set; } = DateTime.MinValue;

        public int Visits { get; set; }

        public decimal AverageBill { get; set; }

        public decimal MaxOrder { get; set; }

        // --- Order History Table ---
        public List<CustomerHistoryRowViewModel> OrderHistory { get; set; } = new List<CustomerHistoryRowViewModel>();
    }

    public class CustomerHistoryRowViewModel
    {
        public DateTime OrderDate { get; set; }

        public string OrderType { get; set; } = string.Empty;

        public string PaymentStatus { get; set; } = string.Empty;

        public int ItemsCount { get; set; }

        public decimal Amount { get; set; }
    }
}
