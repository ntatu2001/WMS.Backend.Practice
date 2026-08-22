namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.InventoryReceiptEntries
{
    public class GetInventoryReceiptEntriesQuery : IRequest<QueryResult<InventoryReceiptEntryDTO>>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? WarehouseName { get; set; }
        public string? LotNumber { get; set; }
        public string? MaterialName { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public GetInventoryReceiptEntriesQuery(DateTime? fromDate = null, DateTime? toDate = null, string? warehouseName = null,
                                                string? lotNumber = null, string? materialName = null, int? pageNumber = null, int? pageSize = null)
        {
            FromDate = fromDate;
            ToDate = toDate;
            WarehouseName = warehouseName;
            LotNumber = lotNumber;
            MaterialName = materialName;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
