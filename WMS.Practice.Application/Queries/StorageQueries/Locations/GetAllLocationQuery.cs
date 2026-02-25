namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetAllLocationQuery : Query, IRequest<QueryResult<LocationDTO>>
    {
        public GetAllLocationQuery(int page, int itemsPerPage)
        {
            Page = page;
            ItemsPerPage = itemsPerPage;
        }
    }
}
