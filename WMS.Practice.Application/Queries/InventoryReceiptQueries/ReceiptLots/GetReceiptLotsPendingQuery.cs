namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.ReceiptLots
{
    public class GetReceiptLotsPendingQuery : IRequest<IEnumerable<ReceiptLotDTO>>
    {
        public string WarehouseId { get; set; }

        public GetReceiptLotsPendingQuery(string warehouseId)
        {
            WarehouseId = warehouseId;
        }
    }
}
