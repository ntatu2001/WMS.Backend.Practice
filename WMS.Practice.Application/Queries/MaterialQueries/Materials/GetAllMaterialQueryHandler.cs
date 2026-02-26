namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetAllMaterialQueryHandler : IRequestHandler<GetAllMaterialQuery, QueryResult<MaterialDTO>>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetAllMaterialQueryHandler(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<QueryResult<MaterialDTO>> Handle(GetAllMaterialQuery request, CancellationToken cancellationToken)
        {
            var materials = await _materialRepository.GetAllMaterialsAsync()
                         ?? throw new EntityNotFoundException("Materials could not found");

            var skipItems = (request.Page - 1) * request.ItemsPerPage;
            var currentItemsOnPage = materials.Skip(skipItems).Take(request.ItemsPerPage).ToList();

            return new QueryResult<MaterialDTO>(results: _mapper.Map<List<MaterialDTO>>(currentItemsOnPage),
                                                totalItems: materials.Count);
        }
    }
}
