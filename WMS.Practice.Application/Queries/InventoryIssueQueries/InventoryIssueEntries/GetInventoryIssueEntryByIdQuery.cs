namespace WMS.Practice.Application.Queries.InventoryIssueQueries.InventoryIssueEntries
{
    public class GetInventoryIssueEntryByIdQuery : IRequest<InventoryIssueEntryDTO>
    {
        public string InventoryIssueEntryId { get; set; }

        public GetInventoryIssueEntryByIdQuery(string inventoryIssueEntryId)
        {
            InventoryIssueEntryId = inventoryIssueEntryId;
        }
    }
}
