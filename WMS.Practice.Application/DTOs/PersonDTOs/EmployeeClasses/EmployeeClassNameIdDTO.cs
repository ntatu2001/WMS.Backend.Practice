namespace WMS.Practice.Application.DTOs.PersonDTOs.EmployeeClasses
{
    public class EmployeeClassNameIdDTO
    {
        public string EmployeeClassId { get; set; }
        public string EmployeeClassName { get; set; }

        public EmployeeClassNameIdDTO(string employeeClassId, string employeeClassName)
        {
            EmployeeClassId = employeeClassId;
            EmployeeClassName = employeeClassName;
        }
    }
}
