using Business.Models;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface ICategoryBusiness
    {
        Task<List<Category>> GetCategories();
        Task<CategoryAddDto> GetCategoryById(int categoryId);
        Task<Category> AddCategory(CategoryAddDto categoryAdd);
        Task<bool> UpdateCategory(int id, CategoryAddDto categoryAdd);
        Task<Category> GetCategoryPatchById(int categoryId);
        Task<bool> PatchCategory(Category category);
        Task<bool> DeleteCategory(Category category);
    }
}
