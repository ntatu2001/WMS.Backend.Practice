namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class StockLocationHistory : Entity, IAggregateModel
    {
        public string StockLocationHistoryId { get; private set; }
        public string MaterialSubLotId { get; private set; }
        public string LotNumber { get; private set; }
        public string LocationId { get; private set; }
        public Location Location { get; private set; }
        public double Quantity { get; private set; }
        public StockMovementType MovementType { get; private set; }
        public DateTime EventDate { get; private set; }

        public StockLocationHistory(string stockLocationHistoryId, string materialSubLotId, string lotNumber, string locationId, double quantity, StockMovementType movementType, DateTime eventDate)
        {
            StockLocationHistoryId = stockLocationHistoryId;
            MaterialSubLotId = materialSubLotId;
            LotNumber = lotNumber;
            LocationId = locationId;
            Quantity = quantity;
            MovementType = movementType;
            EventDate = eventDate;
        }
    }
}
