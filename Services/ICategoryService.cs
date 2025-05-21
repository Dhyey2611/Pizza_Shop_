using Pizza_Shop_.Models;
using Pizza_Shop_.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
// using Pizza_Shop_.ViewModels;

namespace Pizza_Shop_.Services
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        void AddCategory(AddCategoryViewModel model);
        void EditCategory(EditCategoryViewModel model);
        void DeleteCategory(int id);
    }
}