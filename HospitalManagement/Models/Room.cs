namespace HospitalManagement.Models;

public partial class Room
{
    public Guid RoomId { get; set; }

    public string? RoomName { get; set; }

    public Guid? DepartmentId { get; set; }

    public string? RoomType { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<ImagingRequest> ImagingRequests { get; set; } = new List<ImagingRequest>();

    public virtual ICollection<LabRequest> LabRequests { get; set; } = new List<LabRequest>();
}
