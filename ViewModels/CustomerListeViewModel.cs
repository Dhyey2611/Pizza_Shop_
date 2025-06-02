namespace Pizza_Shop_.ViewModels
{
    public class CustomerListViewModel
    {

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public int TotalOrders { get; set; }
    }
}
