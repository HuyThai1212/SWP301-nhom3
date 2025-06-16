using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class InvoiceMedicineDetail
    {
        [Key]
        public Guid InvoiceDetailMedicineId { get; set; }
        public Guid? InvoiceId { get; set; }
        public Guid VisitId { get; set; }
        public Guid PrescriptionId { get; set; }
        public Guid MedicineId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsBilled { get; set; }

        public Medicine Medicine { get; set; }
        public Invoice Invoice { get; set; }
        public Prescription Prescription { get; set; }
    }
}
