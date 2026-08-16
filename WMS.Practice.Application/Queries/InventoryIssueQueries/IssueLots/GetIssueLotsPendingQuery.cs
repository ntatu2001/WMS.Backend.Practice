namespace WMS.Practice.Application.Queries.InventoryIssueQueries.IssueLots
{
    public class GetIssueLotsPendingQuery : IRequest<IEnumerable<IssueLotPendingDTO>>
    {
        public string WarehouseId { get; set; }

        public GetIssueLotsPendingQuery(string warehouseId)
        {
            WarehouseId = warehouseId;
        }
    }
}
