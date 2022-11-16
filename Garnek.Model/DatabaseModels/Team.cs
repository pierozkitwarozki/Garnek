using System;
namespace Garnek.Model.DatabaseModels;

public record Team(Guid Id, DateTime CreatedAt, string Name, ICollection<User> Users, Guid GameId, Game Game, int Points)
    : BaseModel(Id, CreatedAt);


