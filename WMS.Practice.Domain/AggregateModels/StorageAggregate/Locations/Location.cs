namespace WMS.Practice.Domain.AggregateModels.StorageAggregate
{
    public class Location : Entity, IAggregateModel
    {
        public string LocationCode { get; private set; }
        public string LocationName { get; private set; }
        public List<LocationProperty> Properties { get; private set; }
        public string WarehouseId { get; private set; }
        public Warehouse Warehouse { get; private set; }

        public Location(string locationCode, string locationName, string warehouseId)
        {
            LocationCode = locationCode;
            LocationName = locationName;
            WarehouseId = warehouseId;
            Properties = new List<LocationProperty>();
        }
    }
}
