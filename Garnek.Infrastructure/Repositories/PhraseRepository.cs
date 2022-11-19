using Garnek.Application.Repositories;
using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;

namespace Garnek.Infrastructure.Repositories;

public class PhraseRepository : BaseRepository<Phrase>, IPhraseRepository
{
    public PhraseRepository(DatabaseContext context) : base(context)
    {
    }
}