namespace WMS.Practice.Domain.AggregateModels.IssueReceiptAggregate
{
    public class ReceiptSubLot : Entity, IAggregateModel
    {
        public string ReceiptSubLotId { get; private set; }
        public double ImportedQuantity { get; private set; }
        public LotStatus LotStatus { get; private set; }
        public UnitOfMeasure UnitOfMeasure { get; private set; }
        public string LocationId { get; private set; }
        public Location Location { get; private set; }
        public string ReceiptLotId { get; private set; }
        public ReceiptLot ReceiptLot { get; private set; }

        public ReceiptSubLot(string receiptSubLotId, double importedQuantity, LotStatus lotStatus, UnitOfMeasure unitOfMeasure, string locationId, string receiptLotId)
        {
            ReceiptSubLotId = receiptSubLotId;
            ImportedQuantity = importedQuantity;
            LotStatus = lotStatus;
            UnitOfMeasure = unitOfMeasure;
            LocationId = locationId;
            ReceiptLotId = receiptLotId;
        }
    }
}
