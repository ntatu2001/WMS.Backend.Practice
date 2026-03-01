namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.StockTakeTrackings
{
    public class GetStockTakeLotTrackingQuery : TimeRangeQuery, IRequest<List<StockTakeLotTrackingDTO>>
    {
        public string? LotNumber { get; set; }
        public string? MaterialId { get; set; }

        public GetStockTakeLotTrackingQuery(string? lotNumber, string? materialId, DateTime? startTime, DateTime? endTime)
        {
            LotNumber = lotNumber;
            MaterialId = materialId;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
