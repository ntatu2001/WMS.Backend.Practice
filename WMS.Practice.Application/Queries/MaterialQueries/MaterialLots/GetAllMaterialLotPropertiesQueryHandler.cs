namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetAllMaterialLotPropertiesQueryHandler : IRequestHandler<GetAllMaterialLotPropertiesQuery, IEnumerable<MaterialLotPropertyDTO>>
    {
        private readonly IMaterialLotPropertyRepository _materialLotPropertyRepository;
        private readonly IMapper _mapper;

        public GetAllMaterialLotPropertiesQueryHandler(IMaterialLotPropertyRepository materialLotPropertyRepository, IMapper mapper)
        {
            _materialLotPropertyRepository = materialLotPropertyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialLotPropertyDTO>> Handle(GetAllMaterialLotPropertiesQuery request, CancellationToken cancellationToken)
        {
            var materialLotProperties = await _materialLotPropertyRepository.GetAllAsync()
                                     ?? throw new EntityNotFoundException("Material Lot Properties could not found");   

            return _mapper.Map<IEnumerable<MaterialLotPropertyDTO>>(materialLotProperties);
        }
    }
}
