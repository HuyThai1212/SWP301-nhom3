namespace HospitalManagement.Models;

public partial class Staff
{
    public Guid StaffId { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public DateTime? Dob { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Role { get; set; }

    public Guid? DepartmentId { get; set; }

    public Guid? PositionId { get; set; }

    public string? Status { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<ImagingRequest> ImagingRequests { get; set; } = new List<ImagingRequest>();

    public virtual ICollection<LabRequest> LabRequests { get; set; } = new List<LabRequest>();

    public virtual Position? Position { get; set; }

    public virtual Receptionist? Receptionist { get; set; }

    public virtual UserAccount StaffNavigation { get; set; } = null!;

    public virtual Technician? Technician { get; set; }
}
