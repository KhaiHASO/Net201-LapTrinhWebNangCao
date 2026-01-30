using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo04.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        // Thuộc tính Navigation - KHÔNG dùng 'virtual' vì chúng ta thực hiện Explicit Loading thủ công
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        // Khóa ngoại
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
