namespace Pizza_Shop_.ViewModels
{
    public class AddMenuItemViewModel
    {
        public string Name { get; set; } = string.Empty;

        public int MenuItemId { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public bool IsAvailable { get; set; }

        public string? ItemType { get; set; }

        public int Quantity { get; set; }

        public string? Unit { get; set; }

        public decimal? TaxPercentage { get; set; }

        public bool DefaultTax { get; set; }

        public string? ShortCode { get; set; }
        public int? Rate { get; set; }
        public List<ModifierSelectionViewModel> SelectedModifiers { get; set; } = new List<ModifierSelectionViewModel>();

    }
}
