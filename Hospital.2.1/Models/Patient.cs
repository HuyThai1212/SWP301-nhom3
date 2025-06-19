using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Patient
    {
        [Key]
        public Guid patient_id { get; set; }
        public string full_name { get; set; } = string.Empty;
        public string gender { get; set; } = string.Empty;
        public DateTime? dob { get; set; }
        public string phone { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;

        public Guid user_id { get; set; }

    }
}