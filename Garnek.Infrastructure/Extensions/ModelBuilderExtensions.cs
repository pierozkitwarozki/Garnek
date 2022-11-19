using System;
using Garnek.Model.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Garnek.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder BuildPhraseModel(this ModelBuilder builder)
    {
        builder.Entity<Phrase>()
            .HasOne(x => x.User)
            .WithMany(x => x.Phrases)
            .HasForeignKey(x => x.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Phrase>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Phrases)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }

    public static ModelBuilder BuildUserModel(this ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasOne(x => x.Team)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.TeamId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<User>()
            .HasOne(x => x.Game)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.GameId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }
    
}


