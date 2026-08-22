using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialClasses
{
    public class GetAllMaterialClassNameIdQueryHandler : IRequestHandler<GetAllMaterialClassNameIdQuery, IEnumerable<MaterialClassNameIdDTO>>
    {
        private readonly IMaterialClassRepository _materialClassRepository;

        public GetAllMaterialClassNameIdQueryHandler(IMaterialClassRepository materialClassRepository)
        {
            _materialClassRepository = materialClassRepository;
        }

        public async Task<IEnumerable<MaterialClassNameIdDTO>> Handle(GetAllMaterialClassNameIdQuery request, CancellationToken cancellationToken)
        {
            var materialClasses = await _materialClassRepository.GetAllMaterialClassNameIdAsync();

            return materialClasses.Select(mc => new MaterialClassNameIdDTO(mc.MaterialClassId, mc.MaterialClassName));
        }
    }
}
