using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurvivorApi.Data;
using SurvivorApi.Entities;


namespace SurvivorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitorController : ControllerBase
    {
        private readonly SurviorDbContext _context;

        public CompetitorController(SurviorDbContext context)
        {
            _context = context;
        }

        // GET: api/Competitor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompetitorEntity>>> GetCompetitors()
        {
            // Soft delete uygulandığı için sadece IsDeleted = false olanları alıyoruz.
            // .Include(c => c.Category) ile ilişkili Category verisini de yüklüyoruz.
            return await _context.CompetitorEntities
                                .Where(c => !c.IsDeleted)
                                .Include(c => c.Category)
                                .ToListAsync();
        }

        // GET: api/Competitor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompetitorEntity>> GetCompetitor(int id)
        {
            // İlgili yarışmacı Id'ye göre bulunur
            var competitor = await _context.CompetitorEntities
                                        .Where(c => c.Id == id && !c.IsDeleted)
                                        .Include(c => c.Category)
                                        .FirstOrDefaultAsync();

            if (competitor == null)
            {
                return NotFound();
            }

            return competitor;
        }

        // GET: api/Competitor/category/2
        // Talimatlardaki spesifik endpoint
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<CompetitorEntity>>> GetCompetitorsByCategoryId(int categoryId)
        {
            var competitors = await _context.CompetitorEntities
                                        .Where(c => c.CategoryId == categoryId && !c.IsDeleted)
                                        .Include(c => c.Category)
                                        .ToListAsync();

            if (!competitors.Any())
            {
                // Eğer bu kategoriye ait yarışmacı yoksa, boş bir liste dönebiliriz veya NotFound dönebiliriz.
                // Burada boş liste dönmek daha mantıklıdır.
                return NotFound("Bu kategoriye ait yarışmacı bulunamadı.");
            }

            return competitors;
        }


        // POST: api/Competitor
        [HttpPost]
        public async Task<ActionResult<CompetitorEntity>> PostCompetitor(CompetitorEntity competitor)
        {
            // Kategori Id'sinin var olup olmadığını kontrol edelim
            if (!_context.CompetitorEntities.Any(c => c.Id == competitor.CategoryId && !c.IsDeleted))
            {
                return BadRequest("Geçerli bir CategoryId girilmedi.");
            }

            _context.CompetitorEntities.Add(competitor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompetitor", new { id = competitor.Id }, competitor);
        }

        // PUT: api/Competitor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompetitor(int id, CompetitorEntity competitor)
        {
            if (id != competitor.Id)
            {
                return BadRequest();
            }

            // Kategori Id'sinin var olup olmadığını kontrol edelim
            if (!_context.CompetitorEntities.Any(c => c.Id == competitor.CategoryId && !c.IsDeleted))
            {
                return BadRequest("Geçerli bir CategoryId girilmedi.");
            }

            _context.Entry(competitor).State = EntityState.Modified;

            // ModifiedDate alanını otomatik olarak güncelliyoruz.
            competitor.ModifiedDate = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CompetitorEntities.Any(e => e.Id == id && !e.IsDeleted))
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

        // DELETE: api/Competitor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompetitor(int id)
        {
            var competitor = await _context.CompetitorEntities
                                            .Where(c => c.Id == id && !c.IsDeleted)
                                            .FirstOrDefaultAsync();

            if (competitor == null)
            {
                return NotFound();
            }

            // Soft Delete işlemi
            competitor.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
