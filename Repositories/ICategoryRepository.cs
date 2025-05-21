using System.Collections.Generic;
using System.Threading.Tasks;
using Pizza_Shop_.Models;
using Pizza_Shop_.ViewModels;

namespace Pizza_Shop_.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        void AddCategory(Category category);
        void EditCategory(EditCategoryViewModel model);
        void DeleteCategory(int id);
    }
}