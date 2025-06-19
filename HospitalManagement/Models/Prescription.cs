namespace HospitalManagement.Models;

public partial class Prescription
{
    public Guid PrescriptionId { get; set; }

    public Guid? VisitId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<InvoiceMedicineDetail> InvoiceMedicineDetails { get; set; } = new List<InvoiceMedicineDetail>();

    public virtual MedicalVisit? Visit { get; set; }
}
