using Business;
using Business.Models;
using DataAccess.Repository;
using Entities.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreApiCategories.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBusiness _categoryBusiness;

        public CategoriesController(ICategoryBusiness categoryBusiness)
        {
            _categoryBusiness = categoryBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await _categoryBusiness.GetCategories();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryAddDto>> Get(int id)
        {
            CategoryAddDto categoryDto = await _categoryBusiness.GetCategoryById(id);

            if (categoryDto == null)
            {
                return NotFound();
            }

            return categoryDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryAddDto categoryAdd)
        {
            Category categ = await _categoryBusiness.AddCategory(categoryAdd);

            if (categ != null)
            {
                return new CreatedAtRouteResult("GetCategory", new { id = categ.CategoryId }, categ);
            }
            else
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryAddDto categoryAdd)
        {
            bool result = await _categoryBusiness.UpdateCategory(id, categoryAdd);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Category> jsonPatchDocument)
        {
            if (jsonPatchDocument == null)
            {
                return BadRequest();
            }

            Category categoryDto = await _categoryBusiness.GetCategoryPatchById(id);

            if (categoryDto == null)
            {
                return NotFound();
            }

            jsonPatchDocument.ApplyTo(categoryDto, ModelState);

            var isValid = TryValidateModel(categoryDto);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _categoryBusiness.PatchCategory(categoryDto);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Category category = await _categoryBusiness.GetCategoryPatchById(id);

            if (category == null)
            {
                return NotFound();
            }

            bool result = await _categoryBusiness.DeleteCategory(category);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
