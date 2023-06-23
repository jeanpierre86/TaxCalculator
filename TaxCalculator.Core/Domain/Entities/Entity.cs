namespace TaxCalculator.Core.Domain.Entities
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public bool Active { get; set; } = true;
    }
}
