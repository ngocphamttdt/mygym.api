using System.ComponentModel.DataAnnotations;

namespace MyGym.Database.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
