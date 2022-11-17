using Garnek.Model.DatabaseModels;

namespace Garnek.Application.Repositories;

public interface IGameRepository : IBaseRepository<Game>
{
    Task<Game> CreateGameAsync(Game game);
}
