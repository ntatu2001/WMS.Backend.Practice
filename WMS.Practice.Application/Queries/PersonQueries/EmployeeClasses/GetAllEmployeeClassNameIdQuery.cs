using WMS.Practice.Application.DTOs.PersonDTOs.EmployeeClasses;

namespace WMS.Practice.Application.Queries.PersonQueries.EmployeeClasses
{
    public class GetAllEmployeeClassNameIdQuery : IRequest<IEnumerable<EmployeeClassNameIdDTO>>
    {
        public GetAllEmployeeClassNameIdQuery() { }
    }
}
