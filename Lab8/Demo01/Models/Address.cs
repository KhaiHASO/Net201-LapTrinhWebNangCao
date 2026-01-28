using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo01.Models;

public class Address
{
    [Key]
    public int AddressId { get; set; }

    [Required]
    [StringLength(200)]
    public string Street { get; set; } = string.Empty;

    [StringLength(100)]
    public string City { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }
}
