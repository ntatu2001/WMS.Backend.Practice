namespace WMS.Practice.Application.DTOs.PersonDTOs.Employees
{
    public class EmployeeNameIdDTO
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public EmployeeNameIdDTO(string employeeId, string employeeName)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
        }
    }
}
