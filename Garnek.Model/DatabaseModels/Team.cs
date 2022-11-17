namespace Garnek.Model.DatabaseModels;

public class Team : BaseModel
{
    public string Name { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public Guid? GameId { get; set; }
    public virtual Game Game { get; set; }
    public int Points { get; set; }
}