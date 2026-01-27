namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class MaterialLot : Entity, IAggregateModel
    {
        public string LotNumber { get; private set; }
        public LotStatus LotStatus { get; private set; }
        public double ExistingQuantity { get; private set; }
        public string MaterialId { get; private set; }
        public Material Material { get; private set; }
        public List<MaterialLotProperty> Properties { get; private set; }
        public List<MaterialSubLot> SubLots { get; private set; }
        public List<IssueLot> IssueLots { get; private set; }
        public List<StockTake> StockTakes { get; private set; }
        public List<InventoryLog> InventoryLogs { get; private set; }
        public MaterialLot(string lotNumber, LotStatus lotStatus, double existingQuantity, string materialId)
        {
            LotNumber = lotNumber;
            LotStatus = lotStatus;
            ExistingQuantity = existingQuantity;
            MaterialId = materialId;
            Properties = new List<MaterialLotProperty>();
            SubLots = new List<MaterialSubLot>();
        }
    }
}
