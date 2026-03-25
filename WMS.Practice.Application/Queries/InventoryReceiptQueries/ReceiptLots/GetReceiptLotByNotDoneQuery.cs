namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.ReceiptLots
{
    public class GetReceiptLotByNotDoneQuery : IRequest<IEnumerable<ReceiptLotDTO>>
    {
        public string WarehouseId { get; set; }

        public GetReceiptLotByNotDoneQuery(string warehouseId)
        {
            WarehouseId = warehouseId;
        }
    }
}
