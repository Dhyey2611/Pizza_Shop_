using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pizza_Shop_.Data;
using Pizza_Shop_.Models;
using Pizza_Shop_.ViewModels;
namespace Pizza_Shop_.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _context;
        public List<Category> GetAllCategories()
        {
            return _context.Categories.Where(c => c.Status != "Inactive").ToList();
        }

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void EditCategory(EditCategoryViewModel model)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == model.Id && c.Status == "Active");
            if (category != null)
            {
                category.Name = model.Name;
                category.Description = model.Description;
                _context.SaveChanges();
            }
        }
        public void DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id && c.Status != "Inactive");
            {
                if (category != null)
                {
                    category.Status = "Inactive";
                    _context.SaveChanges();
                }
            }
        }
    }
}