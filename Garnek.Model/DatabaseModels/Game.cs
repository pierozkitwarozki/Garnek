using System;
namespace Garnek.Model.DatabaseModels;

public class Game : BaseModel
{
    public ICollection<Team> Teams { get; init; }
}