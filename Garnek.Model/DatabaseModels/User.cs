using System;
namespace Garnek.Model.DatabaseModels;

public record User(string Name, Guid Id, DateTime CreatedAt, ICollection<Phrase> Phrases) : BaseModel(Id, CreatedAt);

