using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NET201Slide8Demo02.Models
{
    // Slide 12: Branch (Chủ thể của Inner Join)
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        [Required]
        [StringLength(100)]
        public string BranchName { get; set; } = string.Empty;

        // Thuộc tính điều hướng: Một Branch có nhiều Student
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }

    // Slide 9: Address (Chủ thể của Left Join)
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [StringLength(200)]
        public string Street { get; set; } = string.Empty;

        // Điều hướng chứa Student được tham chiếu (1-1 hoặc 1-0..1)
        public Student? Student { get; set; }
    }

    // Slide 9 & 12: Student (Thực thể chính)
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // --- Minh họa Inner Join (Slide 12) ---
        // BranchId KHÔNG ĐƯỢC NULL (int), bắt buộc mối quan hệ.
        // Điều này tự nhiên tạo ra hành vi INNER JOIN khi truy vấn nếu được DB thực thi, 
        // mặc dù EF Core thường sử dụng Inner Join cho các mối quan hệ bắt buộc.
        public int BranchId { get; set; }
        
        [ForeignKey("BranchId")]
        public Branch? Branch { get; set; }


        // --- Minh họa Left Join (Slide 9) ---
        // AddressId CÓ THỂ NULL (int?), cho phép mối quan hệ tùy chọn.
        // Điều này cho phép Student tồn tại mà không cần Address.
        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address? Address { get; set; }
    }
}
