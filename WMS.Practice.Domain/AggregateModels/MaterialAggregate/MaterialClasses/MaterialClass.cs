namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class MaterialClass : Entity, IAggregateModel
    {
        public string MaterialClassId { get; set; }
        public string MaterialClassName { get; set; }
        public List<Material> Materials { get; set; }
        public List<MaterialClassProperty> Properties { get; set; }
        public MaterialClass(string materialClassId, string materialClassName)
        {
            MaterialClassId = materialClassId;
            MaterialClassName = materialClassName;
            Properties = new List<MaterialClassProperty>();
        }

        public void UpdateInfo(string? className)
        {
            MaterialClassName = className ?? MaterialClassName;
        }

        public bool UpdateProperty(string? propertyName, string? propertyValue)
        {
            if (string.IsNullOrEmpty(propertyName)) 
                return false;

            var property = Properties.FirstOrDefault(x => x.PropertyName.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
            if (property is null)
                return false;

            property.UpdatePropertyValue(propertyValue);
            return true;
        }
    }
}
