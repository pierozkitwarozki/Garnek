using Garnek.Model.DatabaseModels;

namespace Garnek.Application.Repositories;

public interface IPhraseRepository : IBaseRepository<Phrase>
{
    Task<IEnumerable<Phrase>> GetByGameIdAsync(Guid gameId);
    Task<IEnumerable<Phrase>> GetByUserId(Guid userId);
}