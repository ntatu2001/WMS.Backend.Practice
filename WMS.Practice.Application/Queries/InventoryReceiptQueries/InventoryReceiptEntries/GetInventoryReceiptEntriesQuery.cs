namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.InventoryReceiptEntries
{
    public class GetInventoryReceiptEntriesQuery : IRequest<IEnumerable<InventoryReceiptEntryDTO>>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public GetInventoryReceiptEntriesQuery(DateTime? fromDate = null, DateTime? toDate = null)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}
