namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetMaterialLotPropertyByIdQueryHandler : IRequestHandler<GetMaterialLotPropertyByIdQuery, MaterialLotPropertyDTO>
    {
        private readonly IMaterialClassPropertyRepository _materialClassPropertyRepository;
        private readonly IMapper _mapper;

        public GetMaterialLotPropertyByIdQueryHandler(IMaterialClassPropertyRepository materialClassPropertyRepository, IMapper mapper)
        {
            _materialClassPropertyRepository = materialClassPropertyRepository;
            _mapper = mapper;
        }

        public async Task<MaterialLotPropertyDTO> Handle(GetMaterialLotPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var materialLotProperty = await _materialClassPropertyRepository.GetMaterialClassPropertyByPropertyIdAsync(request.MaterialLotPropertyId)
                                   ?? throw new EntityNotFoundException("Material Lot Property could not found");

            return _mapper.Map<MaterialLotPropertyDTO>(materialLotProperty);
        }
    }
}
