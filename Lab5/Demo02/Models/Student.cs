using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo02.Models
{
    /// <summary>
    /// Entity đại diện cho Sinh viên
    /// Minh họa: Data Annotations cho Validation
    /// </summary>
    [Table("Students")]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Mã sinh viên là bắt buộc")]
        [StringLength(20, ErrorMessage = "Mã sinh viên không được vượt quá 20 ký tự")]
        [Display(Name = "Mã sinh viên")]
        public string StudentCode { get; set; } = string.Empty;

        /// <summary>
        /// Họ tên sinh viên
        /// Validation: Required, StringLength tối đa 50 ký tự
        /// </summary>
        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Họ tên phải từ 3 đến 50 ký tự")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Điểm trung bình
        /// Validation: Range từ 0 đến 10
        /// </summary>
        [Required(ErrorMessage = "Điểm trung bình là bắt buộc")]
        [Range(0, 10, ErrorMessage = "Điểm trung bình phải nằm trong khoảng từ 0 đến 10")]
        [Display(Name = "Điểm trung bình")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal GPA { get; set; }

        /// <summary>
        /// Email sinh viên
        /// Validation: EmailAddress - phải đúng định dạng email
        /// </summary>
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(20)]
        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        // Foreign Key cho ClassRoom
        [Required(ErrorMessage = "Vui lòng chọn lớp học")]
        [Display(Name = "Lớp học")]
        [ForeignKey("ClassRoom")]
        public int ClassRoomId { get; set; }

        // Navigation Property - Nhiều sinh viên thuộc một lớp học
        public virtual ClassRoom? ClassRoom { get; set; }
    }
}
