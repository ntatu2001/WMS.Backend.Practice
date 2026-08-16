using WMS.Practice.Application.DTOs.PersonDTOs.Employees;

namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class GetAllEmployeeNameIdQuery : IRequest<IEnumerable<EmployeeNameIdDTO>>
    {
        public GetAllEmployeeNameIdQuery() { }
    }
}
