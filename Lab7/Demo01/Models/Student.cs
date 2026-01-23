using System.ComponentModel.DataAnnotations;

namespace Demo01.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public int Age { get; set; }

        [StringLength(100)]
        public string Major { get; set; } = string.Empty;
    }
}
