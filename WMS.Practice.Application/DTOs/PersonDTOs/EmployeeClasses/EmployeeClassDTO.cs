namespace WMS.Practice.Application.DTOs.PersonDTOs.EmployeeClasses
{
    public class EmployeeClassDTO
    {
        public string? EmployeeClassId { get; set; }
        public string? EmployeeClassName { get; set; }
        public List<EmployeeClassPropertyDTO> Properties { get; set; } = new List<EmployeeClassPropertyDTO>();

        public EmployeeClassDTO()
        {
        }

        public EmployeeClassDTO(string employeeClassId, string employeeClassName, List<EmployeeClassPropertyDTO> properties)
        {
            EmployeeClassId = employeeClassId;
            EmployeeClassName = employeeClassName;
            Properties = properties;
        }
    }
}
