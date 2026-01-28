using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NET201Slide8Demo02.Models
{
    // Slide 12: Branch (Subject of Inner Join)
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        [Required]
        [StringLength(100)]
        public string BranchName { get; set; } = string.Empty;

        // Navigation Property: One Branch has many Students
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }

    // Slide 9: Address (Subject of Left Join)
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [StringLength(200)]
        public string Street { get; set; } = string.Empty;

        // Navigation containing referenced Student (1-1 or 1-0..1)
        public Student? Student { get; set; }
    }

    // Slide 9 & 12: Student (The main entity)
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // --- Demo Inner Join (Slide 12) ---
        // BranchId is NON-NULLABLE (int), enforcing a Required Relationship.
        // This naturally creates an INNER JOIN behavior when querying if enforced by DB, 
        // though EF Core normally uses Inner Join for Required relationships.
        public int BranchId { get; set; }
        
        [ForeignKey("BranchId")]
        public Branch? Branch { get; set; }


        // --- Demo Left Join (Slide 9) ---
        // AddressId is NULLABLE (int?), allowing Optional Relationship.
        // This allows Student to exist without an Address.
        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address? Address { get; set; }
    }
}
