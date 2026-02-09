namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceiptEntries
{
    public class DeleteInventoryReceiptEntriesCommand : IRequest<bool>
    {
        public string EntryId { get; set; }

        public DeleteInventoryReceiptEntriesCommand(string entryId)
        {
            EntryId = entryId;
        }
    }
}
