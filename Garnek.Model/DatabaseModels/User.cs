using System;
namespace Garnek.Model.DatabaseModels;

public class User : BaseModel
{
    public string Name { get; init; }
    public Guid TeamId { get; init; }
    public Team Team { get; init; }
    public ICollection<Phrase> Phrases { get; init; }
}

