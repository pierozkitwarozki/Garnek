using System;
namespace Garnek.Model.DatabaseModels;

public record Category(string Name, Guid Id, DateTime CreatedAt) : BaseModel(Id, CreatedAt);