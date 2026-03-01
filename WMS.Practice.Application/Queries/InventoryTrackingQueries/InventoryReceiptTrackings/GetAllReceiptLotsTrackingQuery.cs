namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.InventoryReceiptTrackings
{
    public class GetAllReceiptLotsTrackingQuery : TimeRangeQuery, IRequest<List<ReceiptLotsTrackingDTO>>
    {
        public string? LotNumber { get; set; }
        public string? SupplierId { get; set; }

        public GetAllReceiptLotsTrackingQuery(string? lotNumber, string? supplierId, DateTime? startTime, DateTime? endTime)
        {
            LotNumber = lotNumber;
            SupplierId = supplierId;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
