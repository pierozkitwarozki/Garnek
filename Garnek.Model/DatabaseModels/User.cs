using System;
namespace Garnek.Model.DatabaseModels;

public record User(Guid Id, DateTime CreatedAt, string Name,  Guid TeamId, Team Team, ICollection<Phrase> Phrases) : BaseModel(Id, CreatedAt);

