using Pizza_Shop_.Models;
using System.Collections.Generic;

namespace Pizza_Shop_.ViewModels
{
    public class MenuPageViewModel
    {
        public List<MenuItemViewModel> MenuItems { get; set; } = new List<MenuItemViewModel>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<ModifierGroup> ModifierGroups { get; set; } = new List<ModifierGroup>();
        public List<ModifierViewModel.ModifierDetailViewModel> Modifiers { get; set; } = new List<ModifierViewModel.ModifierDetailViewModel>();
        //Added for modifier_pop_up
        public List<Modifier> PaginatedModifiers { get; set; } = new List<Modifier>();
        public int ModifierPage { get; set; }
        public int ModifierTotalItems { get; set; }

        // Optional for pagination tracking
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int? SelectedGroup { get; set; }
    }
}
