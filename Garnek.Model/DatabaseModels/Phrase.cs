namespace Garnek.Model.DatabaseModels;

public class Phrase : BaseModel
{
    public string Name { get; set; }
    public Guid? UserId { get; set; }
    public virtual User User { get; set; }
    public Guid? CategoryId { get; set; }
    public virtual Category Category { get; set; }
}
