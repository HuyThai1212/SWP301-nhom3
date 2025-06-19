namespace HospitalManagement.DTOs
{
    public class PrescriptionDTO
    {
        public Guid PrescriptionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }
        public string Diagnosis { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }

        public DateTime VisitTime { get; set; }
    }
}
