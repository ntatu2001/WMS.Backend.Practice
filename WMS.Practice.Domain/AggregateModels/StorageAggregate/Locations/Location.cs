namespace WMS.Practice.Domain.AggregateModels.StorageAggregate
{
    public class Location : Entity, IAggregateModel
    {
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
    }
}
