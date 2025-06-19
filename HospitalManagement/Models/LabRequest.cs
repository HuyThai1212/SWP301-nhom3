namespace HospitalManagement.Models;

public partial class LabRequest
{
    public Guid LabId { get; set; }

    public Guid? VisitId { get; set; }

    public Guid? RequestedBy { get; set; }

    public Guid? RoomId { get; set; }

    public string? TestType { get; set; }

    public string? Result { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<InvoiceLabDetail> InvoiceLabDetails { get; set; } = new List<InvoiceLabDetail>();

    public virtual Staff? RequestedByNavigation { get; set; }

    public virtual Room? Room { get; set; }

    public virtual MedicalVisit? Visit { get; set; }
}
