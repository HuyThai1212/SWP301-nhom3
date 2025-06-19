namespace Hospital.Models
{
    public class UserAccount
    {
        public Guid user_id { get; set; }
        public string username { get; set; } = string.Empty; // Email or login name
        public string password_hash { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty; // ADMIN / DOCTOR / PHARMACIST / ...
        public bool is_active { get; set; }
        public DateTime? last_login { get; set; }
    }
}
