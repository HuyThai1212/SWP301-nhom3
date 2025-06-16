using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Prescription
    {
        [Key]
        public Guid PrescriptionId { get; set; }
        public Guid VisitId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }

        public ICollection<PrescriptionDetail> PrescriptionDetails { get; set; }
    }
}
