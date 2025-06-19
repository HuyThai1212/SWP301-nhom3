using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Staff
    {
        [Key]
        public Guid staff_id { get; set; }
        public string full_name { get; set; } = string.Empty;
        public string gender { get; set; } = string.Empty;
        public DateTime? dob { get; set; }
        public string phone { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public Guid user_id { get; set; }           // FK đến UserAccount
        public UserAccount? UserAccount { get; set; }
    }
}