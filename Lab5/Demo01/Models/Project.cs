using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo01.Models
{
    /// <summary>
    /// Entity đại diện cho Dự án
    /// Minh họa: Quan hệ N-N với Employee
    /// </summary>
    [Table("Projects")]
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Tên dự án là bắt buộc")]
        [StringLength(150)]
        [Column("ProjectName", TypeName = "nvarchar(150)")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày kết thúc")]
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Ngân sách phải lớn hơn 0")]
        public decimal? Budget { get; set; }

        [StringLength(50)]
        public string? Status { get; set; } // "Planning", "In Progress", "Completed", "Cancelled"

        // Navigation Property - Quan hệ N-N với Employee (thông qua EmployeesInProject)
        // Một dự án có nhiều nhân viên
        public virtual ICollection<EmployeesInProject> EmployeesInProjects { get; set; } = new List<EmployeesInProject>();
    }
}
