namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class GetAllEmployeesQuery : Query, IRequest<QueryResult<EmployeeDTO>>
    {
        public GetAllEmployeesQuery(int page, int itemsPerPage)
        {
            Page = page;
            ItemsPerPage = itemsPerPage;
        }
    }
}
