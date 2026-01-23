using System.ComponentModel.DataAnnotations;

namespace Demo02.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string Status { get; set; } = "New"; // New, Active, Inactive

        public bool IsActive { get; set; }
    }
}
