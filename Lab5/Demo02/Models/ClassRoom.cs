using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo02.Models
{
    /// <summary>
    /// Entity đại diện cho Lớp học
    /// Minh họa: Quan hệ 1-N với Student
    /// </summary>
    [Table("ClassRooms")]
    public class ClassRoom
    {
        [Key]
        public int ClassRoomId { get; set; }

        [Required(ErrorMessage = "Mã lớp là bắt buộc")]
        [StringLength(20, ErrorMessage = "Mã lớp không được vượt quá 20 ký tự")]
        [Display(Name = "Mã lớp")]
        public string ClassCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên lớp là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên lớp không được vượt quá 100 ký tự")]
        [Display(Name = "Tên lớp")]
        public string ClassName { get; set; } = string.Empty;

        // Navigation Property - Quan hệ 1-N với Student
        // Một lớp học có nhiều sinh viên
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
