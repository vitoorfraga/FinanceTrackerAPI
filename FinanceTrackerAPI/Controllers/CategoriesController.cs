using FinanceTrackerAPI.Core;
using FinanceTrackerAPI.Data;
using FinanceTrackerAPI.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController(FinanceTrackerDbContext context) : Controller
    {
        private readonly FinanceTrackerDbContext _context = context;

        [HttpPost(Name = "CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategory category)
        {
            if (category == null)
            {
                return BadRequest("Category is null");
            }

            var newCategory = new Categories
            {
                Name = category.Name
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync(); // Persistindo a mudança no banco de dados

            return Ok(newCategory); // Retornar o objeto criado como resposta
        }


        [HttpGet(Name = "GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            Console.WriteLine("GetCategories");
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }


        [HttpPut("{id}", Name = "UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryById category)
        {
            if (category == null)
            {
                return BadRequest("Invalid category");
            }

            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            existingCategory.Name = category.Name;

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();

            return Ok(existingCategory);
        }

        [HttpDelete("{id}", Name = "DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound("Category not found");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
