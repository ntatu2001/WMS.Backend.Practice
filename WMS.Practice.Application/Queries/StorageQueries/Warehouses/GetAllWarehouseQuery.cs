using WMS.Practice.Application.DTOs.StorageDTOs.Warehouses;

namespace WMS.Practice.Application.Queries.StorageQueries.Warehouses
{
    public class GetAllWarehouseQuery : IRequest<IEnumerable<WarehouseDTO>>
    {
        public GetAllWarehouseQuery() { }
    }
}
