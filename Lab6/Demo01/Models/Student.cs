using System.ComponentModel.DataAnnotations;

namespace Demo01.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Họ tên phải từ 5 đến 50 ký tự")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@fpt\.edu\.vn$", ErrorMessage = "Email phải có đuôi @fpt.edu.vn")]
        public string Email { get; set; }

        [Display(Name = "Tuổi")]
        [Range(18, 40, ErrorMessage = "Tuổi phải từ 18 đến 40")]
        public int Age { get; set; }

        [Display(Name = "Điểm trung bình")]
        [Range(0, 10, ErrorMessage = "Điểm GPA phải từ 0 đến 10")]
        public double Gpa { get; set; }
    }
}
