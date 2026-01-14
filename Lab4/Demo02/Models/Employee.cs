namespace Demo02.Models
{
    // Minh họa quan hệ Một - Nhiều (Dependent Entity)
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        // Foreign Key: Convention (EntityName + PrincipalKey)
        public int DepartmentID { get; set; }

        // Navigation Property: Một nhân viên thuộc về một phòng ban
        public Department Department { get; set; }
    }
}
