namespace WMS.Practice.Application.Queries
{
    public class QueryResult<T>
    {
        public List<T> Results { get; set; }
        public int TotalItems { get; set; }

        public QueryResult(List<T> results, int totalItems)
        {
            Results = results;
            TotalItems = totalItems;
        }
    }
}
