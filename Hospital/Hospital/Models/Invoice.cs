using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Invoice
    {
        [Key]
        public Guid InvoiceId { get; set; }
        public Guid PatientId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime IssuedDate { get; set; }

        public ICollection<InvoiceMedicineDetail> InvoiceMedicineDetails { get; set; }
    }
}
