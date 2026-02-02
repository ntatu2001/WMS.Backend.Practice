using System.Linq.Expressions;
using System.Reflection;

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

        public void UpdatePropertyValue(string? propertyValue)
        {
            PropertyValue = propertyValue ?? PropertyValue;
        }

        public void Update(string? propertyName, string? propertyValue, UnitOfMeasure? unitOfMeasure)
        {
            PropertyName = propertyName ?? PropertyName;
            PropertyValue = propertyValue ?? PropertyValue;
            UnitOfMeasure = unitOfMeasure ?? UnitOfMeasure;
        }

        public void UpdateProperty<T>(Expression<Func<EmployeeProperty, T>> selector, T updateValue)
        {
            if (selector is null || updateValue is null)
                return;

            if ((selector.Body is MemberExpression memberExpression) is false)
                throw new ArgumentException("Body of selector is not MemberExpression", nameof(selector.Body));

            if ((memberExpression.Member is PropertyInfo propertyInfo) is false)
                throw new ArgumentException("Member of Expression is not Property Info", nameof(memberExpression.Member));

            propertyInfo.SetValue(this, updateValue);
        }
    }
}
