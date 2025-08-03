namespace SurvivorApi.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; }

        //Relational Properties
        public List<CompetitorEntity> Competitors { get; set; } = new List<CompetitorEntity>();

        //Category ve competitor arasindaki bire cok iliskiyi list olarak veriyorum yani 1 kategoride birden fazla yarismaci olabilir.
    }
}
