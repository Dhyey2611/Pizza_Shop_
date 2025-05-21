namespace Pizza_Shop_.ViewModels
{
    public class RoleListViewModel
    {
        public List<RoleItemViewModel> Roles { get; set; } = new();
    }

    public class RoleItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}