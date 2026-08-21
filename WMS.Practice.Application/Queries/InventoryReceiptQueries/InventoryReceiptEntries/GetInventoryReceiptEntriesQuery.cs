namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.InventoryReceiptEntries
{
    public class GetInventoryReceiptEntriesQuery : IRequest<IEnumerable<InventoryReceiptEntryDTO>>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? WarehouseName { get; set; }

        public GetInventoryReceiptEntriesQuery(DateTime? fromDate = null, DateTime? toDate = null, string? warehouseName = null)
        {
            FromDate = fromDate;
            ToDate = toDate;
            WarehouseName = warehouseName;
        }
    }
}
