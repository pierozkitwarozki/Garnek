using Garnek.Application.Repositories;
using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;

namespace Garnek.Infrastructure.Repositories;

public class TeamRepository : BaseRepository<Team>, ITeamRepository
{
    public TeamRepository(DatabaseContext context) : base(context)
    {
    }
}