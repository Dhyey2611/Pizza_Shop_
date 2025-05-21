namespace Pizza_Shop_.ViewModels
{
    public class UsersPageViewModel
    {
        public List<UserListViewModel> Users { get; set; } = new List<UserListViewModel>();
        public int CurrentPage { get; set; }
        public int TotalUsers { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
    }
}
