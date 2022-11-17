using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;

namespace Garnek.Application.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
{
    private readonly DatabaseContext _context;

    public BaseRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> AddEntityAsync(T entity)
    {
        await _context.AddAsync<T>(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddEnitiesAsync(IEnumerable<T> entities)
    {
        await _context.AddRangeAsync(entities);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<T> UpdateEntityAsync(T entity)
    {
        _context.Update(entity);

        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<T>> UpdateEntitiesAsync(IEnumerable<T> entities)
    {
        _context.UpdateRange(entities);

        await _context.SaveChangesAsync();

        return entities;
    }

    public async Task<T> GetEntityByIdAsync(Guid id)
    {
        var entity = await _context.FindAsync<T>(id);

        return entity;
    }

    public abstract Task<ICollection<T>> GetAllAsync();
}