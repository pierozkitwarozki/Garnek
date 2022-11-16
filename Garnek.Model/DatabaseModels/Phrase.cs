using System;
namespace Garnek.Model.DatabaseModels;

public record Phrase(string Name, Guid UserId, User User, Guid CategoryId, Category Category,  Guid Id, DateTime CreatedAt) : BaseModel(Id, CreatedAt);
