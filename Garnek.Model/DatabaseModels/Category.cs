namespace Garnek.Model.DatabaseModels;

public class Category : BaseModel
{
    public string Name { get; set; }
    public virtual ICollection<Phrase> Phrases { get; set; }
}