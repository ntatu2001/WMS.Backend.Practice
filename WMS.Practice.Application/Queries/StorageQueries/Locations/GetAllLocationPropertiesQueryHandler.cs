namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetAllLocationPropertiesQueryHandler : IRequestHandler<GetAllLocationPropertiesQuery, IEnumerable<LocationPropertyDTO>>
    {

        private readonly ILocationPropertyRepository _locationPropertyRepository;
        private readonly IMapper _mapper;

        public GetAllLocationPropertiesQueryHandler(ILocationPropertyRepository locationPropertyRepository, IMapper mapper)
        {
            _locationPropertyRepository = locationPropertyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationPropertyDTO>> Handle(GetAllLocationPropertiesQuery request, CancellationToken cancellationToken)
        {
            var locationProperties = await _locationPropertyRepository.GetAllLocationProperties()
                                  ?? throw new EntityNotFoundException(nameof(LocationProperty));

            var locationPropertiesDTOs = _mapper.Map<IEnumerable<LocationPropertyDTO>>(locationProperties);
            return locationPropertiesDTOs;
        }
    }
}
