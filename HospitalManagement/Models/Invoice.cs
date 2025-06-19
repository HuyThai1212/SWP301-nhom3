namespace HospitalManagement.Models;

public partial class Invoice
{
    public Guid InvoiceId { get; set; }

    public Guid? PatientId { get; set; }

    public decimal? Amount { get; set; }

    public string? Status { get; set; }

    public DateTime? IssuedDate { get; set; }

    public virtual ICollection<InvoiceConsultDetail> InvoiceConsultDetails { get; set; } = new List<InvoiceConsultDetail>();

    public virtual ICollection<InvoiceImageDetail> InvoiceImageDetails { get; set; } = new List<InvoiceImageDetail>();

    public virtual ICollection<InvoiceLabDetail> InvoiceLabDetails { get; set; } = new List<InvoiceLabDetail>();

    public virtual ICollection<InvoiceMedicineDetail> InvoiceMedicineDetails { get; set; } = new List<InvoiceMedicineDetail>();

    public virtual Patient? Patient { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
