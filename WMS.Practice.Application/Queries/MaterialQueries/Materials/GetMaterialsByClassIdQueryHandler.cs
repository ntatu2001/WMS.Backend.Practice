namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetMaterialsByClassIdQueryHandler : IRequestHandler<GetMaterialsByClassIdQuery, IEnumerable<MaterialDTO>>
    {
        private readonly IMaterialClassRepository _materialClassRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;
        public GetMaterialsByClassIdQueryHandler(IMaterialClassRepository materialClassRepository, IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialClassRepository = materialClassRepository;
            _materialRepository = materialRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MaterialDTO>> Handle(GetMaterialsByClassIdQuery request, CancellationToken cancellationToken)
        {
            var materialClass = await _materialClassRepository.GetMaterialClassByClassIdAsync(request.MaterialClassId)
                             ?? throw new EntityNotFoundException($"Material class could not found for the given id {request.MaterialClassId}");

            var materials = await _materialRepository.GetMaterialsByClassIdAsync(request.MaterialClassId)
                         ?? throw new EntityNotFoundException($"Materials could not found for the given class id {request.MaterialClassId}");

            var materialDTOs = _mapper.Map<IEnumerable<MaterialDTO>>(materials);
            foreach (var materialDTO in materialDTOs)
            {
                materialDTO.MaterialName = materialClass.MaterialClassName;
            }

            return materialDTOs;
        }
    }
}
