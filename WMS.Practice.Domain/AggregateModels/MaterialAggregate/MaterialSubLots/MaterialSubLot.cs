namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class MaterialSubLot : Entity, IAggregateModel
    {
        public string MaterialSubLotId { get; private set; }
        public LotStatus SubLotStatus { get; private set; }
        public double ExistingQuantity { get; private set; }
        public UnitOfMeasure UnitOfMeasure  { get; private set; }
        public string LocationId { get; private set; }
        public Location Location { get; private set; }
        public string LotNumber { get; private set; }
        public MaterialLot MaterialLot { get; private set; }
        public List<IssueSubLot> IssueSubLots { get; private set; }

        public MaterialSubLot(string materialSubLotId, LotStatus subLotStatus, double existingQuantity, string locationId, string lotNumber, UnitOfMeasure unitOfMeasure)
        {
            MaterialSubLotId = materialSubLotId;
            SubLotStatus = subLotStatus;
            ExistingQuantity = existingQuantity;
            LocationId = locationId;
            LotNumber = lotNumber;
            UnitOfMeasure = unitOfMeasure;
        }

        public void Update(LotStatus? subLotStatus, double? existingQuality, UnitOfMeasure? unitOfMeasure, string? locationId)
        {
            SubLotStatus = subLotStatus ?? SubLotStatus;
            ExistingQuantity = existingQuality ?? ExistingQuantity;
            UnitOfMeasure = unitOfMeasure ?? UnitOfMeasure;
            LocationId = locationId ?? LocationId;
        }

        public void UpdateExistingQuantity(double existingQuantity)
        {
            ExistingQuantity = existingQuantity;
        }
    }
}
