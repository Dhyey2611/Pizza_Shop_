namespace Pizza_Shop_.ViewModels
{
    public class OrderListViewModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string PaymentMode { get; set; } = string.Empty;
        public int Rating { get; set; }
        public decimal TotalAmount { get; set; }
    }
}