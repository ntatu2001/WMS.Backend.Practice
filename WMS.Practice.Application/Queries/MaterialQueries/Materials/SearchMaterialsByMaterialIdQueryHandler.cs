namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class SearchMaterialsByMaterialIdQueryHandler : IRequestHandler<SearchMaterialsByMaterialIdQuery, QueryResult<MaterialDTO>>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public SearchMaterialsByMaterialIdQueryHandler(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<QueryResult<MaterialDTO>> Handle(SearchMaterialsByMaterialIdQuery request, CancellationToken cancellationToken)
        {
            var materialsQuery = _materialRepository.QueryMaterials();

            if (!string.IsNullOrWhiteSpace(request.MaterialId))
            {
                materialsQuery = materialsQuery.Where(m => m.MaterialId.Contains(request.MaterialId));
            }

            if (!string.IsNullOrWhiteSpace(request.MaterialClassId))
            {
                materialsQuery = materialsQuery.Where(m => m.MaterialClassId == request.MaterialClassId);
            }

            materialsQuery = materialsQuery.OrderBy(m => m.MaterialId);

            var totalItems = await materialsQuery.CountAsync(cancellationToken);

            var skipItems = (request.Page - 1) * request.ItemsPerPage;
            var currentItemsOnPage = await materialsQuery.Skip(skipItems).Take(request.ItemsPerPage).ToListAsync(cancellationToken);

            return new QueryResult<MaterialDTO>(results: _mapper.Map<List<MaterialDTO>>(currentItemsOnPage),
                                                totalItems: totalItems);
        }
    }
}
