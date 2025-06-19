namespace HospitalManagement.Models;

public partial class Doctor
{
    public Guid StaffId { get; set; }

    public string? Specialization { get; set; }

    public string? LicenseNo { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<MedicalVisit> MedicalVisits { get; set; } = new List<MedicalVisit>();

    public virtual Staff Staff { get; set; } = null!;
}
