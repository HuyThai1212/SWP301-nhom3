namespace HospitalManagement.Models;

public partial class Appointment
{
    public Guid AppointmentId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? DoctorId { get; set; }

    public Guid? RoomId { get; set; }

    public DateTime? ScheduledTime { get; set; }

    public string? Status { get; set; }

    public Guid? CreatedBy { get; set; }

    public string? CreatedRole { get; set; }

    public Guid? ApprovedBy { get; set; }

    public string? ApprovalStatus { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public virtual Receptionist? ApprovedByNavigation { get; set; }

    public virtual UserAccount? CreatedByNavigation { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<MedicalVisit> MedicalVisits { get; set; } = new List<MedicalVisit>();

    public virtual Patient? Patient { get; set; }

    public virtual Room? Room { get; set; }
}
