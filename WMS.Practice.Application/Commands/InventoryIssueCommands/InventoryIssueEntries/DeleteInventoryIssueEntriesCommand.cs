namespace WMS.Practice.Application.Commands.InventoryIssueCommands.InventoryIssueEntries
{
    public class DeleteInventoryIssueEntriesCommand : IRequest<bool>
    {
        public string InventoryEntryId { get; set; }

        public DeleteInventoryIssueEntriesCommand(string entryId)
        {
            InventoryEntryId = entryId;
        }
    }
}
