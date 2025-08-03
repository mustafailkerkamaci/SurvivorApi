namespace SurvivorApi.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; } //? null- bos deger olabilir.
        public bool IsDeleted { get; set; }




    }
}
