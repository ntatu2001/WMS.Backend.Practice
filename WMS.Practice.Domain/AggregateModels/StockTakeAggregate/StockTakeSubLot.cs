namespace WMS.Practice.Domain.AggregateModels.StockTakeAggregate
{
    public class StockTakeSubLot : Entity, IAggregateModel
    {
        public string StockTakeSubLotId { get; private set; }
        public string LocationId { get; private set; }
        public string MaterialSubLotId { get; private set; }
        public double PreviousQuantity { get; set; }
        public double AdjustedQuantity { get; set; }
        public double QuantityDifference {  get; set; }
        public string StockTakeId { get; private set; }
        public StockTake StockTake { get; private set; }

        public StockTakeSubLot(string stockTakeSubLotId, string locationId, string materialSubLotId, string stockTakeId, 
                               double previousQuantity, double adjustedQuantity, double quantityDifference)
        {
            StockTakeSubLotId = stockTakeSubLotId;
            LocationId = locationId;
            MaterialSubLotId = materialSubLotId;
            StockTakeId = stockTakeId;
            PreviousQuantity = previousQuantity;
            AdjustedQuantity = adjustedQuantity;
            QuantityDifference = quantityDifference;
        }
    }
}
