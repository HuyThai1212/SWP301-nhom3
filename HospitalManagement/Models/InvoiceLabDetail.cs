namespace HospitalManagement.Models;

public partial class InvoiceLabDetail
{
    public Guid InvoiceDetailLabId { get; set; }

    public Guid? InvoiceId { get; set; }

    public Guid VisitId { get; set; }

    public Guid LabId { get; set; }

    public string Description { get; set; } = null!;

    public decimal Amount { get; set; }

    public bool IsBilled { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual LabRequest Lab { get; set; } = null!;

    public virtual MedicalVisit Visit { get; set; } = null!;
}
