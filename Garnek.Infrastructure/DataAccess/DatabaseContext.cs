using Garnek.Infrastructure.Extensions;
using Garnek.Model.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Garnek.Infrastructure.DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder
            .BuildPhraseModel()
            .BuildUserModel();
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Phrase> Phrases { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<User> Users { get; set; }
}
