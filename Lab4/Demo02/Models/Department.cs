using System.Collections.Generic;

namespace Demo02.Models
{
    // Minh họa quan hệ Một - Nhiều (Principal Entity)
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        // Navigation Property: Một phòng ban có nhiều nhân viên
        public ICollection<Employee> Employees { get; set; }
    }
}
