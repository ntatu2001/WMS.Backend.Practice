namespace WMS.Practice.Application.Queries.PersonQueries.Suppliers
{
    public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, IEnumerable<SupplierDTO>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public GetAllSuppliersQueryHandler(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDTO>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _supplierRepository.GetAllAsync()
                         ?? throw new EntityNotFoundException("Could not get Suppliers");

            var supplierDTOs = _mapper.Map<IEnumerable<SupplierDTO>>(suppliers);
            return supplierDTOs;
        }
    }
}
