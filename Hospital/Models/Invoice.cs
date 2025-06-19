using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class Invoice
    {
        [Key]
        public Guid invoice_id { get; set; }

        [Required]
        public Guid patient_id { get; set; }

        [Required]
        public decimal amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string? status { get; set; } 

        public DateTime issued_date { get; set; }
    }
}
