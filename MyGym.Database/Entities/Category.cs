namespace MyGym.Database.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
