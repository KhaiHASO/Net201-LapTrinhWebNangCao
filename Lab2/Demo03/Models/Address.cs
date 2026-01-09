using System.ComponentModel.DataAnnotations;

namespace Demo03.Models
{
    public class Address
    {
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; }
    }
}
