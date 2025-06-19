namespace HospitalManagement.Models;

public partial class Department
{
    public Guid DepartmentId { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
