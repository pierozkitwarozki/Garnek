using Garnek.Application.Repositories;
using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Garnek.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await Context.Users.ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUsersForTeamAsync(Guid teamId)
    {
        return await Context
            .Users
            .Where(user => user.TeamId == teamId)
            .ToListAsync();
    }
}

