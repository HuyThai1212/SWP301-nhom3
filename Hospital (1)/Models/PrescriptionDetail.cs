using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class PrescriptionDetail
    {
        [Key]
        public Guid PrescriptionId { get; set; }
        public Guid MedicineId { get; set; }
        public int Quantity { get; set; }
        public string Dosage { get; set; }

        public Medicine Medicine { get; set; }
        public Prescription Prescription { get; set; }
    }
}
