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

        public void Update(LotStatus? lotStatus, double? existingQuantity)
        {
            LotStatus = lotStatus ?? LotStatus;
            ExistingQuantity = existingQuantity ?? ExistingQuantity;
        }

        public bool TryUpdateProperty(string? propertyId, string? propertyName, string? propertyValue, UnitOfMeasure? unitOfMeasure)
        {
            if (string.IsNullOrEmpty(propertyId)) 
                return false;

            var property = Properties?.FirstOrDefault(x => x.PropertyId == propertyId);
            if (property is null)
                return false;

            property.Update(propertyName, propertyValue, unitOfMeasure);
            return true;
        }

        public bool TryUpdateSubLot(string? subLotId, LotStatus? subLotStatus, double? existingQuantity, UnitOfMeasure? unitOfMeasure, string? locationId)
        {
            if (string.IsNullOrEmpty(subLotId))
                return false;

            var subLot = SubLots?.FirstOrDefault(x => x.MaterialSubLotId == subLotId);
            if (subLot is null) 
                return false;

            subLot.Update(subLotStatus, existingQuantity, unitOfMeasure, locationId);
            return true;
        }

        public void UpdateExistingQuantity(double totalQuantity)
        {
            ExistingQuantity = totalQuantity;
        }

        public void AddSubLot(MaterialSubLot newSubLot)
        {
            SubLots ??= new List<MaterialSubLot>();
            SubLots.Add(newSubLot);
        }
    }
}
