namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class Material : Entity, IAggregateModel
    {
        #region Constants

        private const string volumePackagePropertyName = "VolumePacket";
        private const string packetSizePropertyName = "PacketSize";

        #endregion

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

        public bool HasProperties() => Properties?.Count > 0;

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

        public bool TryGetPropertyValue(string propertyName, out string propertyValue)
        {
            propertyValue = string.Empty;
            if (HasProperties() is false)
                return false;

            var property = Properties.FirstOrDefault(p => p.PropertyName == propertyName);
            if (property is null)
                return false;

            propertyValue = property.PropertyValue;
            return true;
        }

        public bool TryCalculateUsedVolume(double existingQuantity, out double usedVolume)
        {
            usedVolume = 0.0;
            if (TryGetPacketSize(out var packetSize) is false || TryGetVolumePackage(out var volumePackage) is false)
                return false;

            usedVolume = (existingQuantity / packetSize) * volumePackage;
            return true;
        }

        private bool TryGetVolumePackage(out double volumePackage)
        {
            volumePackage = 0.0;
            if (TryGetPropertyValue(volumePackagePropertyName, out string volumePackageStr) is false || double.TryParse(volumePackageStr.Trim(), out volumePackage))
                return false;

            return true;
        }

        private bool TryGetPacketSize(out double packageSize)
        {
            packageSize = 0.0;
            if (TryGetPropertyValue(packetSizePropertyName, out string packageSizeStr) is false || double.TryParse(packageSizeStr.Trim(), out packageSize))
                return false;

            return true;
        }
    }
}
