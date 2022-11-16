using System;
namespace Garnek.Model.DatabaseModels;

public record Phrase(string Name, Guid UserId, Guid CategoryId, Guid Id, DateTime CreatedAt) : BaseModel(Id, CreatedAt);
