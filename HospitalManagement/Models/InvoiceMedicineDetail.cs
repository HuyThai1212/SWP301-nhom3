namespace HospitalManagement.Models;

public partial class InvoiceMedicineDetail
{
    public Guid InvoiceDetailMedicineId { get; set; }

    public Guid? InvoiceId { get; set; }

    public Guid VisitId { get; set; }

    public Guid PrescriptionId { get; set; }

    public Guid MedicineId { get; set; }

    public string Description { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalAmount { get; set; }

    public bool IsBilled { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;

    public virtual Prescription Prescription { get; set; } = null!;

    public virtual MedicalVisit Visit { get; set; } = null!;
}
