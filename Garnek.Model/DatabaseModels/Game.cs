using System;
namespace Garnek.Model.DatabaseModels;

public class Game : BaseModel
{
    public virtual ICollection<Team> Teams { get; init; }
}