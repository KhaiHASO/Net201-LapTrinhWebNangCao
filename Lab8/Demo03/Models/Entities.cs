using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo03.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    // VIRTUAL cho Lazy Loading
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    [StringLength(100)]
    public string OrderName { get; set; } = string.Empty;

    public int? CustomerId { get; set; }
    
    // VIRTUAL cho Lazy Loading
    [ForeignKey("CustomerId")]
    public virtual Customer? Customer { get; set; }
}
