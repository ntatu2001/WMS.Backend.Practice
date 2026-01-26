namespace WMS.Practice.Domain.AggregateModels.PersonAggregate
{
    public class EmployeeClassProperty : Entity, IAggregateModel
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string EmployeeClassId { get; set; }
        public EmployeeClass EmployeeClass { get; set; }

        public EmployeeClassProperty(string propertyId, string propertyName, string propertyValue, UnitOfMeasure unitOfMeasure, string employeeClassId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            EmployeeClassId = employeeClassId;
        }
    }
}
