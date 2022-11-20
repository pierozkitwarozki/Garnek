using Garnek.Model.DatabaseModels;

namespace Garnek.Application.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByNameAsync(string name);
}