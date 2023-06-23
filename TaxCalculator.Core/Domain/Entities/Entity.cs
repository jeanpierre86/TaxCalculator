using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Core.Domain.Entities
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public bool Active { get; set; } = true;
    }
}
