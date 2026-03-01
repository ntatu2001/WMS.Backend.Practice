namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.InventoryReceipts
{
    public class GetInventoryReceiptByIdQuery : IRequest<InventoryReceiptDTO>
    {
        public string InventoryReceiptId { get; set; }

        public GetInventoryReceiptByIdQuery(string inventoryReceiptId)
        {
            InventoryReceiptId = inventoryReceiptId;
        }
    }
}
