using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurvivorApi.Data;
using SurvivorApi.Entities;

namespace SurvivorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SurviorDbContext _context;

        public CategoryController(SurviorDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryEntity>>>GetCategories()
        {
            //soft delete uytgulandigi icin sadece IsDeleted false olanlari aliyorum
            return await _context.CategoryEntities.Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        //GET: api/Category/id
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryEntity>> GetCategory(int id)
        {
            var category = await _context.CategoryEntities
                .Where( c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();
            if (category == null )
            {
                return NotFound();
            }
            return category;
        }


        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryEntity>> PostCategory(CategoryEntity category)
        {
            _context.CategoryEntities.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        //PUT :api Category/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryEntity category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            _context.Entry(category).State = EntityState.Modified;
            category.ModifiedDate = DateTime.Now; // Güncelleme tarihini ayarlıyorum
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CategoryEntities.Any(e => e.Id == id && !e.IsDeleted))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        //DELETE : api/Category/id

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.CategoryEntities
                                        .Where(c => c.Id == id && !c.IsDeleted)
                                        .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }

            // Soft Delete işlemi
            category.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
