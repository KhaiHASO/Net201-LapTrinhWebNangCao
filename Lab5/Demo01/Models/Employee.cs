using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo01.Models
{
    /// <summary>
    /// Entity đại diện cho Nhân viên
    /// Minh họa: ForeignKey, Multiple Relationships
    /// </summary>
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [StringLength(100)]
        [Column("FullName", TypeName = "nvarchar(100)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 1000000000, ErrorMessage = "Lương phải từ 0 đến 1 tỷ")]
        public decimal? Salary { get; set; }

        // Foreign Key cho Department (Quan hệ N-1)
        [ForeignKey("Department")] // Chỉ định Navigation Property
        public int? DepartmentId { get; set; }

        // Navigation Property - Nhiều nhân viên thuộc một phòng ban
        public virtual Department? Department { get; set; }

        // Navigation Property - Quan hệ 1-1 với EmployeeAddress
        public virtual EmployeeAddress? EmployeeAddress { get; set; }

        // Navigation Property - Quan hệ N-N với Project (thông qua EmployeesInProject)
        public virtual ICollection<EmployeesInProject> EmployeesInProjects { get; set; } = new List<EmployeesInProject>();
    }
}
