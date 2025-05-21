namespace Pizza_Shop_.ViewModels
{
    public class UserListViewModel
    {
         public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty; // Role
        public string Status { get; set; } = "Active";
    }
}
