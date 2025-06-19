namespace HospitalManagement.Models;

public partial class Payment
{
    public Guid PaymentId { get; set; }

    public Guid? InvoiceId { get; set; }

    public string? PaymentMethod { get; set; }

    public decimal? PaidAmount { get; set; }

    public DateTime? PaidAt { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
