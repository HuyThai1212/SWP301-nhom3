namespace HospitalManagement.DTOs
{
    public class PrescriptionDetailDTO
    {
        public Guid PrescriptionId { get; set; }
        public Guid MedicineId { get; set; }
        public string MedicineName { get; set; } = null!;
        public int Quantity { get; set; }
        public string Dosage { get; set; } = null!;
    }
}
