using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialClasses
{
    public class GetAllMaterialClassesQueryHandler : IRequestHandler<GetAllMaterialClassesQuery, IEnumerable<MaterialClassDTO>>
    {
        private readonly IMaterialClassRepository _materialClassRepository;
        private readonly IMapper _mapper;
        public GetAllMaterialClassesQueryHandler(IMaterialClassRepository materialClassRepository, IMapper mapper)
        {
            _materialClassRepository = materialClassRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialClassDTO>> Handle(GetAllMaterialClassesQuery request, CancellationToken cancellationToken)
        {
            var materialClasses = await _materialClassRepository.GetAllAsync()
                               ?? throw new EntityNotFoundException("Material Classes could not found");

            return _mapper.Map<IEnumerable<MaterialClassDTO>>(materialClasses);
        }
    }
}
