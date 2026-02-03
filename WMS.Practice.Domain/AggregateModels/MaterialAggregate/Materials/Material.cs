namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class Material : Entity, IAggregateModel
    {
        public string MaterialId { get; private set; }
        public string MaterialName { get; private set; }
        public string MaterialClassId { get; private set; }
        public MaterialClass MaterialClass { get; private set; }
        public List<MaterialProperty> Properties { get; private set; }
        public List<MaterialLot> MaterialLots { get; private set; }
        public List<ReceiptLot> ReceiptLots { get; private set; }
        public List<IssueLot> IssueLots { get; private set; }
        public Material(string materialId, string materialName, string materialClassId)
        {
            MaterialId = materialId;
            MaterialName = materialName;
            MaterialClassId = materialClassId;
            Properties = new List<MaterialProperty>();
        }

        public void UpdateMaterialInfo(string? materialName, string? materialClassId)
        {
            MaterialName = materialName ?? MaterialName;
            MaterialClassId = materialClassId ?? MaterialClassId;
        }

        public bool TryUpdateProperty(string? propertyName, string? propertyValue)
        {
            if (string.IsNullOrEmpty(propertyName))
                return false;

            var property = Properties.FirstOrDefault(p => p.PropertyName == propertyName);
            if (property is null)
                return false;

            property.UpdatePropertyValue(propertyValue);
            return true;
        }
    }
}
