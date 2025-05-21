using System.Collections.ObjectModel;

namespace Pizza_Shop_.ViewModels
{
    public class ModifierViewModel
    {
        public string ModifierGroupName { get; set; } = string.Empty;
        public List<ModifierDetailViewModel> Modifiers { get; set; } = new List<ModifierDetailViewModel>();

        public class ModifierDetailViewModel
        {
            public int ModifierId { get; set; }
            public int ModifierGroupId { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public string Unit { get; set; } = string.Empty;
        }
    }
}