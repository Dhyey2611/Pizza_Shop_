using Pizza_Shop_.Models;
using Pizza_Shop_.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;
using Pizza_Shop_.ViewModels;
using Pizza_Shop_.Repositories;
namespace Pizza_Shop_.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }
        public void AddCategory(AddCategoryViewModel model)
        {
            var category = new Category
            {
                Name = model.Name,
                Description = model.Description,
                Status = "Active"
            };
            _categoryRepository.AddCategory(category);
        }
        public void EditCategory(EditCategoryViewModel model)
        {
            _categoryRepository.EditCategory(model);
        }
        public void DeleteCategory(int id)
        {
            _categoryRepository.DeleteCategory(id);
        }
    }
}