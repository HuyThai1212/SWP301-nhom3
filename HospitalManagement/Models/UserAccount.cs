namespace HospitalManagement.Models;

public partial class UserAccount
{
    public Guid UserId { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? Role { get; set; }

    public string? Status { get; set; }

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual Staff? Staff { get; set; }
}
