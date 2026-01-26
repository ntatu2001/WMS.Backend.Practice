namespace WMS.Practice.Domain.AggregateModels.PersonAggregate
{
    public class EmployeeProperty : Entity, IAggregateModel
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee{ get; set; }

        public EmployeeProperty(string propertyId, string propertyName, string propertyValue, UnitOfMeasure unitOfMeasure, string employeeId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            EmployeeId = employeeId;
        }

    }
}
