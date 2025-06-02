using System.Collections.Generic;

namespace Pizza_Shop_.ViewModels
{
    public class PaginatedCustomerListViewModel
    {
        public required List<CustomerListViewModel> Customers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
    }
}
