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
            var materialsQuery = _materialRepository.QueryMaterials()
                                                      .OrderBy(m => m.MaterialId);

            var totalItems = await materialsQuery.CountAsync(cancellationToken);

            var skipItems = (request.Page - 1) * request.ItemsPerPage;
            var currentItemsOnPage = await materialsQuery.Skip(skipItems).Take(request.ItemsPerPage).ToListAsync(cancellationToken);

            return new QueryResult<MaterialDTO>(results: _mapper.Map<List<MaterialDTO>>(currentItemsOnPage),
                                                totalItems: totalItems);
        }
    }
}
