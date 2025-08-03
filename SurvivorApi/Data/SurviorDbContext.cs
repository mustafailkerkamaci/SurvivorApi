using Microsoft.EntityFrameworkCore;
using SurvivorApi.Entities;
namespace SurvivorApi.Data
{
    public class SurviorDbContext : DbContext
    {
        public SurviorDbContext(DbContextOptions<SurviorDbContext> options) : base(options)
        {
        }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }
        public DbSet<CompetitorEntity> CompetitorEntities { get; set; }


    }
}
