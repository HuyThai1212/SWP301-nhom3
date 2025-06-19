namespace HospitalManagement.Models;

public partial class PrescriptionDetail
{
    public Guid? PrescriptionId { get; set; }

    public Guid? MedicineId { get; set; }

    public int? Quantity { get; set; }

    public string? Dosage { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual Prescription? Prescription { get; set; }
}
