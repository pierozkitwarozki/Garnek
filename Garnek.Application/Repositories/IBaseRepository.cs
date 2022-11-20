using Garnek.Model.DatabaseModels;

namespace Garnek.Application.Repositories;

public interface IBaseRepository<T> where T : BaseModel
{
    Task<bool> AddEntityAsync(T entity);
    Task<bool> AddEntitiesAsync(IEnumerable<T> entities);
    Task<T> UpdateEntityAsync(T entity);
    Task<IEnumerable<T>> UpdateEntitiesAsync(IEnumerable<T> entities);
    Task<T> GetEntityByIdAsync(Guid id);
    Task<bool> DeleteByIdAsync(Guid id);}

