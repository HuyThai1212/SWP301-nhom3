namespace HospitalManagement.Models;

public partial class Position
{
    public Guid PositionId { get; set; }

    public string? Title { get; set; }

    public Guid? CategoryId { get; set; }

    public int? Rank { get; set; }

    public string? Description { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
