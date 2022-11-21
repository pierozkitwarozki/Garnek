using Garnek.Model.DatabaseModels;

namespace Garnek.Application.Repositories;

public interface IBaseRepository<T> where T : BaseModel
{
    Task<bool> AddAsync(T entity);
    Task<bool> AddRangeAsync(IEnumerable<T> entities);
    Task<T> UpdateAsync(T entity);
    Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
    Task<T> GetByIdAsync(Guid id);
    Task<bool> DeleteByIdAsync(Guid id);}

