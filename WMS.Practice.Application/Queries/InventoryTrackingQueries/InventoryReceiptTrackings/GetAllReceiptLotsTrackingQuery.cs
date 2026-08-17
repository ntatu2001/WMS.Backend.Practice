namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.InventoryReceiptTrackings
{
    public class GetAllReceiptLotsTrackingQuery : TimeRangeQuery, IRequest<List<ReceiptLotsTrackingDTO>>
    {
        public string? LotNumber { get; set; }
        public string? SupplierName { get; set; }

        public GetAllReceiptLotsTrackingQuery(string? lotNumber, string? supplierName, DateTime? startTime, DateTime? endTime)
        {
            LotNumber = lotNumber;
            SupplierName = supplierName;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
