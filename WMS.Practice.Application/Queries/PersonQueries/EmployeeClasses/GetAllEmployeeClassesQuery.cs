using WMS.Practice.Application.DTOs.PersonDTOs.EmployeeClasses;

namespace WMS.Practice.Application.Queries.PersonQueries.EmployeeClasses
{
    public class GetAllEmployeeClassesQuery : IRequest<IEnumerable<EmployeeClassDTO>>
    {
        public GetAllEmployeeClassesQuery()
        {
        }
    }
}
