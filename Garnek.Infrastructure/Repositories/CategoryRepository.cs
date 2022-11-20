using Garnek.Application.Repositories;
using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Garnek.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = await Context.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await Context.Categories
            .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

    }
}