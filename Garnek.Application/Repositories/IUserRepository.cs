using Garnek.Model.DatabaseModels;

namespace Garnek.Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<IEnumerable<User>> GetUsersForTeamAsync(Guid teamId);
}


