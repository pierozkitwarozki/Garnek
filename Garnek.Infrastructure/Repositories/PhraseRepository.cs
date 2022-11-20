using Garnek.Application.Repositories;
using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Garnek.Infrastructure.Repositories;

public class PhraseRepository : BaseRepository<Phrase>, IPhraseRepository
{
    public PhraseRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Phrase>> GetByGameIdAsync(Guid gameId)
    {
        return await Context.Phrases
            .Where(x => x.User.GameId == gameId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Phrase>> GetByUserId(Guid userId)
    {
        return await Context.Phrases
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }
}