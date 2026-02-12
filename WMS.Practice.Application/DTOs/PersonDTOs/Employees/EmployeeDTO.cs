namespace WMS.Practice.Application.DTOs.PersonDTOs.Employees
{
    public class EmployeeDTO
    {
        public string? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeCLassId { get; set; }
        public List<EmployeePropertyDTO> EmployeePropertyDTOs { get; set; } = new List<EmployeePropertyDTO>();

        public EmployeeDTO()
        {

        }

        public EmployeeDTO(string employeeId, string employeeName, string employeeCLassId, List<EmployeePropertyDTO> employeePropertyDTOs)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            EmployeeCLassId = employeeCLassId;
            EmployeePropertyDTOs = employeePropertyDTOs;
        }
    }
}
