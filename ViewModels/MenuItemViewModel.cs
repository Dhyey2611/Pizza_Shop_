namespace Pizza_Shop_.ViewModels
{
    public class MenuItemViewModel
    {
        public string Category { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ItemTypeIcon { get; set; } = "~/images/type_icon.png"; // Replace per item
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public bool Available { get; set; }
        public int MenuItemId { get; set; }
        public int CategoryId { get; set; } 
    }
}
