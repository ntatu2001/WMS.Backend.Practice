using WMS.Practice.Application.DTOs.StorageDTOs.Warehouses;

namespace WMS.Practice.Application.Queries.StorageQueries.Warehouses
{
    public class GetAllWarehouseNameIdQuery : IRequest<IEnumerable<WarehouseNameIdDTO>>
    {
        public GetAllWarehouseNameIdQuery() { }
    }
}
