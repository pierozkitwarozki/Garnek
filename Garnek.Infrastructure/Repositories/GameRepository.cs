using Garnek.Application.Repositories;
using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;

namespace Garnek.Infrastructure.Repositories;

public class GameRepository : BaseRepository<Game>, IGameRepository
{
    public GameRepository(DatabaseContext context) : base(context)
    {
    }
}