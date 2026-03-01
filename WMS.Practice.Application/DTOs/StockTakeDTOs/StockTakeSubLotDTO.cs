namespace WMS.Practice.Application.DTOs.StockTakeDTOs
{
    public class StockTakeSubLotDTO
    {
        public string StockTakeSubLotId { get; set; }
        public string LocationId { get; set; }
        public string MaterialSublotId { get; set; }
        public double PreviousQuantity { get; set; }
        public double RealAdjustmentQuantity { get; set; }
        public double QuantityDifference { get; set; }
        public string StockTakeLotId { get; set; }

        public StockTakeSubLotDTO(string stockTakeSubLotId, string locationId, string materialSublotId, double previousQuantity, double realAdjustmentQuantity, double quantityDifference, string stockTakeLotId)
        {
            StockTakeSubLotId = stockTakeSubLotId;
            LocationId = locationId;
            MaterialSublotId = materialSublotId;
            PreviousQuantity = previousQuantity;
            RealAdjustmentQuantity = realAdjustmentQuantity;
            QuantityDifference = quantityDifference;
            StockTakeLotId = stockTakeLotId;
        }
    }
}
