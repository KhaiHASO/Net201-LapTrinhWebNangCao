using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo01.Models
{
    /// <summary>
    /// Entity trung gian cho quan hệ Many-to-Many
    /// Minh họa: Composite Key (sẽ cấu hình bằng Fluent API)
    /// </summary>
    [Table("EmployeesInProjects")]
    public class EmployeesInProject
    {
        // Composite Primary Key (EmployeeId + ProjectId)
        // Sẽ được cấu hình trong DbContext bằng Fluent API
        
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày tham gia")]
        public DateTime JoinedDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? Role { get; set; } // Vai trò trong dự án: "Developer", "Tester", "Manager"...

        [Range(0, 100)]
        public int? WorkloadPercentage { get; set; } // Phần trăm công việc phân bổ

        // Navigation Properties
        public virtual Employee Employee { get; set; } = null!;
        public virtual Project Project { get; set; } = null!;
    }
}
