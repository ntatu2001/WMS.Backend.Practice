namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class SearchMaterialsByMaterialIdQuery : Query, IRequest<QueryResult<MaterialDTO>>
    {
        public string? MaterialId { get; set; }
        public string? MaterialClassId { get; set; }

        public SearchMaterialsByMaterialIdQuery(string? materialId, string? materialClassId, int page, int itemsPerPage)
        {
            MaterialId = materialId;
            MaterialClassId = materialClassId;
            Page = page;
            ItemsPerPage = itemsPerPage;
        }
    }
}
