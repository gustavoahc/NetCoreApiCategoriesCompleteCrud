using AutoMapper;
using Business.Models;
using DataAccess.Repository;
using Entities.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger _logger;
        private readonly IMapper mapper = AutoMapperConfiguration.GetMapperProperty();

        public CategoryBusiness(ICategoryRepository categoryRepository, ILogger<CategoryBusiness> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _categoryRepository.GetCategories();
        }

        public async Task<CategoryAddDto> GetCategoryById(int categoryId)
        {
            try
            {
                Category category = await _categoryRepository.GetCategoryById(categoryId);
                CategoryAddDto categoryDto = mapper.Map<CategoryAddDto>(category);
                return categoryDto;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public async Task<Category> GetCategoryPatchById(int categoryId)
        {
            try
            {
                Category category = await _categoryRepository.GetCategoryById(categoryId);
                return category;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public async Task<Category> AddCategory(CategoryAddDto categoryAdd)
        {
            Category newCategory = mapper.Map<Category>(categoryAdd);

            return await _categoryRepository.AddCategory(newCategory);
        }

        public async Task<bool> UpdateCategory(int id, CategoryAddDto categoryAdd)
        {
            Category newCategory = mapper.Map<Category>(categoryAdd);
            newCategory.CategoryId = id;
            bool result;
            try
            {
                int resultUpdate = await _categoryRepository.UpdateCategory(newCategory);

                if (resultUpdate > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                result = false;
            }
            return result;
        }

        public async Task<bool> PatchCategory(Category category)
        {
            bool result;
            try
            {
                int resultUpdate = await _categoryRepository.UpdateCategory(category);

                if (resultUpdate > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                result = false;
            }
            return result;
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            bool result;
            try
            {
                int resultUpdate = await _categoryRepository.DeleteCategory(category);

                if (resultUpdate > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                result = false;
            }
            return result;
        }
    }
}
