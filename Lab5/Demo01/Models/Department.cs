using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo01.Models
{
    /// <summary>
    /// Entity đại diện cho Phòng ban
    /// Minh họa: Data Annotations cơ bản
    /// </summary>
    [Table("Departments")] // Chỉ định tên bảng trong database
    public class Department
    {
        [Key] // Đánh dấu là Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Tên phòng ban là bắt buộc")] // NOT NULL
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên phòng ban phải từ 3-100 ký tự")]
        [Column("DepartmentName", TypeName = "nvarchar(100)")] // Chỉ định tên cột và kiểu dữ liệu
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string? Description { get; set; } // Nullable - cho phép NULL

        [DataType(DataType.Date)]
        [Display(Name = "Ngày thành lập")]
        public DateTime? EstablishedDate { get; set; }

        // Navigation Property - Quan hệ 1-N với Employee
        // Một phòng ban có nhiều nhân viên
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
