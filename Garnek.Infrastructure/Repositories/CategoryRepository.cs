using Garnek.Application.Repositories;
using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;

namespace Garnek.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DatabaseContext context) : base(context)
    {
    }
}