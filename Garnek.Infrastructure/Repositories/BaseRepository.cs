using Garnek.Application.Repositories;
using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;

namespace Garnek.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
{
    protected readonly DatabaseContext Context;

    protected BaseRepository(DatabaseContext context)
    {
        Context = context;
    }

    public async Task<bool> AddAsync(T entity)
    {
        await Context.AddAsync<T>(entity);
        return await Context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
    {
        await Context.AddRangeAsync(entities);
        return await Context.SaveChangesAsync() > 0;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        Context.Update(entity);

        await Context.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
    {
        Context.UpdateRange(entities);

        await Context.SaveChangesAsync();

        return entities;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var entity = await Context.FindAsync<T>(id);

        return entity;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var entity = await Context.FindAsync<T>(id);

        if (entity is null)
        {
            return false;
        }

        Context.Remove<T>(entity);

        return await Context.SaveChangesAsync() > 0;
    }
}