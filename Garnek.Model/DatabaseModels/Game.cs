namespace Garnek.Model.DatabaseModels;

public class Game : BaseModel
{
    public virtual ICollection<User> Users { get; set; }
}