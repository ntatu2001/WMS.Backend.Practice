namespace WMS.Practice.Application.Queries.InventoryIssueQueries.IssueLots
{
    public class GetIssueLotByNotDoneQuery : IRequest<IEnumerable<IssueLotDTO>>
    {
        public string WarehouseId { get; set; }

        public GetIssueLotByNotDoneQuery(string warehouseId)
        {
            WarehouseId = warehouseId;
        }
    }
}
