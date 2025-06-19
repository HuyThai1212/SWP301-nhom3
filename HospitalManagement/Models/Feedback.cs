namespace HospitalManagement.Models;

public partial class Feedback
{
    public Guid FeedbackId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? DoctorId { get; set; }

    public int? Score { get; set; }

    public string? Comment { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
