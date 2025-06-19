namespace HospitalManagement.Models;

public partial class Receptionist
{
    public Guid StaffId { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Staff Staff { get; set; } = null!;
}
