using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo01.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    [StringLength(100)]
    public string OrderName { get; set; } = string.Empty;

    public int? CustomerId { get; set; } // Nullable FK
    
    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }
}
