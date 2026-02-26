namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetAllMaterialQuery : Query, IRequest<QueryResult<MaterialDTO>>
    {
        public GetAllMaterialQuery(int page, int itemsPerPage)
        {
            Page = page;
            ItemsPerPage = itemsPerPage;
        }
    }
}
