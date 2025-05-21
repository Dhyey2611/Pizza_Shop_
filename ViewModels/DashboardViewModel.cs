namespace Pizza_Shop_.ViewModels
{
    public class DashboardViewModel
    {
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public decimal AvgOrderValue { get; set; }
        public decimal AvgWaitingTime { get; set; }

        public List<decimal>? RevenueData { get; set; }
        public List<decimal>? CustomerGrowthData { get; set; }

        public List<SellingItem>? TopSellingItems { get; set; }
        public List<SellingItem>? LeastSellingItems { get; set; }

        public int WaitingListCount { get; set; }
        public int NewCustomersCount { get; set; }
    }

    public class SellingItem
    {
        public string? Name { get; set; }
        public int OrdersCount { get; set; }
        public int Rank{get; set;}
    }
}
