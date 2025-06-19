namespace HospitalManagement.Models;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
}
