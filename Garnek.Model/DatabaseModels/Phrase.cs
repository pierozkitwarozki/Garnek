using System;
namespace Garnek.Model.DatabaseModels;

public class Phrase : BaseModel
{
    public string Name { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
    public Guid CategoryId { get; init; }
    public Category Category { get; init; }
}
