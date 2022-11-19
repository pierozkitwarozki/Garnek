namespace Garnek.Model.DatabaseModels;

public class User : BaseModel
{
    public string Name { get; set; }
    public Guid? TeamId { get; set; }
    public Guid? GameId { get; set; }
    public virtual Team Team { get; set; }
    public virtual Game Game { get; set; }
    public virtual ICollection<Phrase> Phrases { get; set; }
}

