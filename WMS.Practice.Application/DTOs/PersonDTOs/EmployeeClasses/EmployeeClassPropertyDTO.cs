namespace WMS.Practice.Application.DTOs.PersonDTOs.EmployeeClasses
{
    public class EmployeeClassPropertyDTO
    {
        public string? PropertyId { get; set; }
        public string? PropertyName { get; set; }
        public string? PropertyValue { get; set; }
        public string? UnitOfMeasure { get; set; }
        public string? EmployeeClassId { get; set; }

        public EmployeeClassPropertyDTO()
        {
        }

        public EmployeeClassPropertyDTO(string propertyId, string propertyName, string propertyValue, string unitOfMeasure, string employeeClassId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            EmployeeClassId = employeeClassId;
        }
    }
}
