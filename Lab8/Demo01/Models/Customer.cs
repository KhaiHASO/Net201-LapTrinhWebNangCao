using System.ComponentModel.DataAnnotations;

namespace Demo01.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    // Thuộc tính điều hướng
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}
