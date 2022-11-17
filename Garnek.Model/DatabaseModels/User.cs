using System;
namespace Garnek.Model.DatabaseModels;

public class User : BaseModel
{
    public string Name { get; init; }
    public Guid? TeamId { get; init; }
    public virtual Team Team { get; init; }
    public virtual ICollection<Phrase> Phrases { get; init; }
}

