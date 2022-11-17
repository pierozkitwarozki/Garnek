using System;
namespace Garnek.Model.DatabaseModels;

public class Phrase : BaseModel
{
    public string Name { get; init; }
    public Guid? UserId { get; init; }
    public virtual User User { get; init; }
    public Guid? CategoryId { get; init; }
    public virtual Category Category { get; init; }
}
