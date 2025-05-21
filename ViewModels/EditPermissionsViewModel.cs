namespace Pizza_Shop_.ViewModels
{
    public class EditPermissionsViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }= string.Empty;
        public List<PermissionItemViewModel> Permissions { get; set; }=new();
    }

    public class PermissionItemViewModel
    {
        public int Id { get; set; }
        public string Module { get; set; } =string.Empty;

        public bool IsSelected { get; set; }
        public bool CanView { get; set; }
        public bool CanAddEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
