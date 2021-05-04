using Entities.Context;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId.Equals(categoryId));
        }

        public async Task<int> UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteCategory(Category category)
        {
            _context.Remove(category);
            return await _context.SaveChangesAsync();
        }
    }
}
