using Api006.Core.Entities.Base;

namespace Api006.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
