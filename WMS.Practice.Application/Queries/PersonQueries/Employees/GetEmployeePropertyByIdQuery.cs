namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class GetEmployeePropertyByIdQuery : IRequest<EmployeePropertyDTO>
    {
        public string EmployeePropertyId { get; set; }

        public GetEmployeePropertyByIdQuery(string employeePropertyId)
        {
            EmployeePropertyId = employeePropertyId;
        }
    }
}
