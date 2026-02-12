namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDTO>
    { 
        public string EmployeeId { get; set; }
        public GetEmployeeByIdQuery(string employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
