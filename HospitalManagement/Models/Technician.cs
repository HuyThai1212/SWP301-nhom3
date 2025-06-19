namespace HospitalManagement.Models;

public partial class Technician
{
    public Guid StaffId { get; set; }

    public string? TechType { get; set; }

    public string? Qualification { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
