using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo01.Models
{
    /// <summary>
    /// Entity đại diện cho Địa chỉ nhân viên
    /// Minh họa: Quan hệ 1-1 (One-to-One)
    /// </summary>
    [Table("EmployeeAddresses")]
    public class EmployeeAddress
    {
        // Primary Key đồng thời là Foreign Key (Shared Primary Key pattern)
        [Key]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        [StringLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string Street { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string City { get; set; } = string.Empty;

        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? Province { get; set; }

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? Country { get; set; }

        // Navigation Property - Quan hệ 1-1 với Employee
        // Một địa chỉ thuộc về một nhân viên duy nhất
        public virtual Employee Employee { get; set; } = null!;
    }
}
