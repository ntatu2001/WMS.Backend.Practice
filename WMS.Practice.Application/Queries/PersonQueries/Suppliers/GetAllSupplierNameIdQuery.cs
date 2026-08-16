using WMS.Practice.Application.DTOs.PersonDTOs.Suppliers;

namespace WMS.Practice.Application.Queries.PersonQueries.Suppliers
{
    public class GetAllSupplierNameIdQuery : IRequest<IEnumerable<SupplierNameIdDTO>>
    {
        public GetAllSupplierNameIdQuery() { }
    }
}
