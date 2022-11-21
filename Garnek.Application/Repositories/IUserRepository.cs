using Garnek.Model.DatabaseModels;

namespace Garnek.Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<IEnumerable<User?>> GetUsersForTeamAsync(Guid teamId);

    Task<IEnumerable<User?>> GetAllAsync();
    Task<User?> GetByNameAndGameIdAsync(Guid gameId, string name);
}


