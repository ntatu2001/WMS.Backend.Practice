using WMS.Practice.Domain.AggregateModels.StorageAggregate;

namespace WMS.Practice.Domain.AggregateModels.PersonAggregate
{
    public class EmployeeClassProperty : Entity, IAggregateModel
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string EmployeeClassId { get; set; }
        public EmployeeClass EmployeeClass { get; set; }

        public EmployeeClassProperty(string propertyId, string propertyName, UnitOfMeasure unitOfMeasure, string employeeClassId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            UnitOfMeasure = unitOfMeasure;
            EmployeeClassId = employeeClassId;
        }
    }
}
