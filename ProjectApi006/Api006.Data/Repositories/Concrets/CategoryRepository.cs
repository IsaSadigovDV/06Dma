using Api006.Data.Context;
using Api006.Core.Entities;
using Api006.Core.Repositories.Abstractions;

namespace Api006.Data.Repositories.Concrets
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
