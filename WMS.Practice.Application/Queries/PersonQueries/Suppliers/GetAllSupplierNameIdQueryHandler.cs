using WMS.Practice.Application.DTOs.PersonDTOs.Suppliers;

namespace WMS.Practice.Application.Queries.PersonQueries.Suppliers
{
    public class GetAllSupplierNameIdQueryHandler : IRequestHandler<GetAllSupplierNameIdQuery, IEnumerable<SupplierNameIdDTO>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetAllSupplierNameIdQueryHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<SupplierNameIdDTO>> Handle(GetAllSupplierNameIdQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _supplierRepository.GetAllSupplierNameIdAsync();

            return suppliers.Select(s => new SupplierNameIdDTO(s.SupplierId, s.SupplierName));
        }
    }
}
