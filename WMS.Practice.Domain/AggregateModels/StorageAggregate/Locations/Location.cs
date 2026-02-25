namespace WMS.Practice.Domain.AggregateModels.StorageAggregate
{
    public class Location : Entity, IAggregateModel
    {
        #region Constants

        private const string lengthPropertyName = "Length";
        private const string widthPropertyName = "Width";
        private const string heightPropertyName = "Height";

        #endregion

        public string LocationId { get; private set; }
        public List<LocationProperty> Properties { get; private set; }
        public string WarehouseId { get; private set; }
        public Warehouse Warehouse { get; private set; }
        public List<MaterialSubLot> MaterialSubLots { get; private set; }
        public List<ReceiptSubLot> ReceiptSubLots { get; private set; }
        public List<IssueSubLot> IssueSubLots { get; private set; }
        public Location(string locationId, string warehouseId, List<LocationProperty>? properties = null)
        {
            LocationId = locationId;
            WarehouseId = warehouseId;
            Properties = properties ?? new List<LocationProperty>();
        }

        public void UpdateWarehouse(string warehouseId)
        {
            WarehouseId = warehouseId;
        }

        public bool HasProperties() => Properties?.Count > 0;

        public bool HasMaterialSubLots() => MaterialSubLots?.Count > 0;

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

        public bool TryCalculateLocationMaxVolume(out double maxVolume)
        {
            maxVolume = 0.0;
            if (TryGetPropertyValue(lengthPropertyName, out string lengthStr) is false || double.TryParse(lengthStr.Trim(), out double length))
                return false;

            if (TryGetPropertyValue(widthPropertyName, out string widthStr) is false || double.TryParse(widthStr.Trim(), out double width))
                return false;

            if (TryGetPropertyValue(heightPropertyName, out string heightStr) is false || double.TryParse(heightStr.Trim(), out double height))
                return false;

            maxVolume = length * width * height;
            return true;
        }

        public string GetLengthValue() => TryGetPropertyValue(lengthPropertyName, out string lengthStr) ? lengthStr : string.Empty;

        public string GetWidthValue() => TryGetPropertyValue(widthPropertyName, out string widthStr) ? widthStr : string.Empty;

        public string GetHeightValue() => TryGetPropertyValue(heightPropertyName, out string heightStr) ? heightStr : string.Empty;
    }
}
