using System;
namespace Garnek.Model.DatabaseModels;

public class Team : BaseModel
{
    public string Name { get; init; }
    public ICollection<User> Users { get; init; }
    public Guid GameId { get; init; }
    public Game Game { get; init; }
    public int Points { get; init; }
}