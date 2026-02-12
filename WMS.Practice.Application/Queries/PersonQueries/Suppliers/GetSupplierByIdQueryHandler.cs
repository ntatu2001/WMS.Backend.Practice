namespace WMS.Practice.Application.Queries.PersonQueries.Suppliers
{
    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, SupplierDTO>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public GetSupplierByIdQueryHandler(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<SupplierDTO> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var existingSupplier = await _supplierRepository.GetSupplierByIdAsync(request.SupplierId)
                                ?? throw new EntityNotFoundException($"Supplier {request.SupplierId} could not found", nameof(request.SupplierId));

            var supplierDTO = _mapper.Map<SupplierDTO>(existingSupplier);
            return supplierDTO;
        }
    }
}
