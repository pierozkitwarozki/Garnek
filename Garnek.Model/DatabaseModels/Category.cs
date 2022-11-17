using System;
namespace Garnek.Model.DatabaseModels;

public class Category : BaseModel
{
    public string Name { get; init; }
    public virtual ICollection<Phrase> Phrases { get; init; }
}