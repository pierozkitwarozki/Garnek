using System;
namespace Garnek.Model.DatabaseModels;

public class Team : BaseModel
{
    public string Name { get; init; }
    public virtual ICollection<User> Users { get; init; }
    public Guid? GameId { get; init; }
    public virtual Game Game { get; init; }
    public int Points { get; init; }
}