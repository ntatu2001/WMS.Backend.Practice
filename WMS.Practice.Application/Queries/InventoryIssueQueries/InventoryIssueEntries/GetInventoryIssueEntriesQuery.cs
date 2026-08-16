namespace WMS.Practice.Application.Queries.InventoryIssueQueries.InventoryIssueEntries
{
    public class GetInventoryIssueEntriesQuery : IRequest<IEnumerable<InventoryIssueEntryDTO>>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public GetInventoryIssueEntriesQuery(DateTime? fromDate = null, DateTime? toDate = null)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}
