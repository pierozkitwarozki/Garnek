using System;
namespace Garnek.Model.DatabaseModels;

public record Game(Guid Id, DateTime CreatedAt, ICollection<Team> Teams) : BaseModel(Id, CreatedAt);