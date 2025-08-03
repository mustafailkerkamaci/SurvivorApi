using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SurvivorApi.Entities
{
    public class CompetitorEntity :BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CategoryId { get; set; }

        //Relational Properties
        public CategoryEntity Category { get; set; } //Category ile Competitor arasindaki iliskiyi tanimliyorum. 1 yarismaci 1 kategoride olabilir.



    }
}
