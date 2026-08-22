namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class SearchEmployeesByEmployeeIdQuery : Query, IRequest<QueryResult<EmployeeDTO>>
    {
        public string? EmployeeId { get; set; }
        public string? EmployeeClassId { get; set; }

        public SearchEmployeesByEmployeeIdQuery(string? employeeId, string? employeeClassId, int page, int itemsPerPage)
        {
            EmployeeId = employeeId;
            EmployeeClassId = employeeClassId;
            Page = page;
            ItemsPerPage = itemsPerPage;
        }
    }
}
