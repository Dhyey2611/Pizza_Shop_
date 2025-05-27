namespace Pizza_Shop_.ViewModels
{
    public class PaginatedOrderListViewModel
    {
        public List<OrderListViewModel> Orders { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
    }
}