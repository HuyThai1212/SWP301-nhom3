namespace HospitalManagement.Models;

public partial class Medicine
{
    public Guid MedicineId { get; set; }

    public string? Name { get; set; }

    public string? Unit { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<InvoiceMedicineDetail> InvoiceMedicineDetails { get; set; } = new List<InvoiceMedicineDetail>();
}
