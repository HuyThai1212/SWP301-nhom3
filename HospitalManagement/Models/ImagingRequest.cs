namespace HospitalManagement.Models;

public partial class ImagingRequest
{
    public Guid ImageId { get; set; }

    public Guid? VisitId { get; set; }

    public Guid? RequestedBy { get; set; }

    public Guid? RoomId { get; set; }

    public string? ImageType { get; set; }

    public string? ResultDescription { get; set; }

    public virtual ICollection<InvoiceImageDetail> InvoiceImageDetails { get; set; } = new List<InvoiceImageDetail>();

    public virtual Staff? RequestedByNavigation { get; set; }

    public virtual Room? Room { get; set; }

    public virtual MedicalVisit? Visit { get; set; }
}
