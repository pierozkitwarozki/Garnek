using System;
namespace Garnek.Model.DatabaseModels;

public record Team(string Name, ICollection<User> Users, int Points, Guid Id, DateTime CreatedAt)
    : BaseModel(Id, CreatedAt);


