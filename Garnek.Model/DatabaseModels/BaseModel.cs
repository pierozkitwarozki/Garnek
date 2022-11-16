using System;
namespace Garnek.Model.DatabaseModels;

public class BaseModel
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}