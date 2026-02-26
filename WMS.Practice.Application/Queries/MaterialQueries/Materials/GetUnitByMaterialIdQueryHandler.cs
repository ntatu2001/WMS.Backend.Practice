namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetUnitByMaterialIdQueryHandler : IRequestHandler<GetUnitByMaterialIdQuery, string>
    {
        private readonly IMaterialRepository _materialRepository;

        public GetUnitByMaterialIdQueryHandler(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<string> Handle(GetUnitByMaterialIdQuery request, CancellationToken cancellationToken)
        {
            var material = await _materialRepository.GetMaterialByIdAsync(request.MaterialId)
                        ?? throw new EntityNotFoundException($"Material with Id {request.MaterialId} could not found");  

            return material.TryGetPropertyValue("Unit", out string unit) ? unit : string.Empty;
        }
    }
}
