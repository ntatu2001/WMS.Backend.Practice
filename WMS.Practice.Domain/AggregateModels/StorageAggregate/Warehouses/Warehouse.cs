namespace WMS.Practice.Domain.AggregateModels.StorageAggregate
{
    public class Warehouse : Entity, IAggregateModel
    {
        public string WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public List<Location> Locations { get; set; }
        public List<WarehouseProperty> Properties { get; set; }
        public List<InventoryReceipt> InventoryReceipts { get; set; }
        public List<InventoryIssue> InventoryIssues { get; set; }
        public List<StockTake> StockTakes { get; set; }
        public List<InventoryLog> InventoryLogs { get; set; }
        public Warehouse(string warehouseId, string warehouseName)
        {
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
            Properties = new List<WarehouseProperty>();
        }

        public void UpdateWarehouse(string warehouseId, string warehouseName)
        {
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
        }
    }
}
