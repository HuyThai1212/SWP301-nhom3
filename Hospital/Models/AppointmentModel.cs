namespace Hospital.Models
{
    public class AppointmentModel
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime ScheduledTime { get; set; }
    }
}
