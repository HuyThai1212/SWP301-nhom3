namespace HospitalManagement.Models;

public partial class Patient
{
    public Guid PatientId { get; set; }

    public Guid? UserId { get; set; }

    public string? FullName { get; set; }

    public DateTime? Dob { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? InsuranceId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<MedicalVisit> MedicalVisits { get; set; } = new List<MedicalVisit>();

    public virtual UserAccount? User { get; set; }
}
