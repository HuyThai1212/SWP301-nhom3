using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    [Table("Medicine")]
    public class Medicine
    {
        [Key]
        public Guid Medicine_Id { get; set; } 

        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
    }
}
