namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.InventoryReceiptEntries
{
    public class GetInventoryReceiptEntryByIdQuery : IRequest<InventoryReceiptEntryDTO>
    {
        public string InventoryReceiptEntryId { get; set; }

        public GetInventoryReceiptEntryByIdQuery(string inventoryReceiptEntryId)
        {
            InventoryReceiptEntryId = inventoryReceiptEntryId;
        }
    }
}
