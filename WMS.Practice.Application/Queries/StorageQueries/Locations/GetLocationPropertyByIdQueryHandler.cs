namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetLocationPropertyByIdQueryHandler : IRequestHandler<GetLocationPropertyByIdQuery, LocationPropertyDTO>
    {
        private readonly ILocationPropertyRepository _locationPropertyRepository;
        private readonly IMapper _mapper;

        public GetLocationPropertyByIdQueryHandler(ILocationPropertyRepository locationPropertyRepository, IMapper mapper)
        {
            _locationPropertyRepository = locationPropertyRepository;
            _mapper = mapper;
        }

        public async Task<LocationPropertyDTO> Handle(GetLocationPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var locationProperty = await _locationPropertyRepository.GetLocationPropertyByIdAsync(request.LocationPropertyId)
                                ?? throw new EntityNotFoundException(nameof(LocationProperty), request.LocationPropertyId);

            var locationPropertyDTO = _mapper.Map<LocationPropertyDTO>(locationProperty);
            return locationPropertyDTO;
        }
    }
}
