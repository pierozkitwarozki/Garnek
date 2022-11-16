using System;
namespace Garnek.Model.DatabaseModels;

public record Category(Guid Id, DateTime CreatedAt, string Name, ICollection<Phrase> Phrases) : BaseModel(Id, CreatedAt);