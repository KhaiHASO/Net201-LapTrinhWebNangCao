using System;
using System.ComponentModel.DataAnnotations;

namespace demo01.Models
{
    public class Information
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string License { get; set; }
        public DateTime Established { get; set; }
        public decimal Revenue { get; set; }
    }
}
