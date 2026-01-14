using System.Collections.Generic;

namespace Demo02.Models
{
    // Minh họa Convention Rules: Không dùng Data Annotations
    public class Customer
    {
        public int CustomerID { get; set; } // PK by convention
        public string CustomerName { get; set; }
        public string PhoneNo { get; set; }
    }
}
