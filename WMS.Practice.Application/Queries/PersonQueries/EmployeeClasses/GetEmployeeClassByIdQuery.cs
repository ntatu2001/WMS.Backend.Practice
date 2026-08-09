using WMS.Practice.Application.DTOs.PersonDTOs.EmployeeClasses;

namespace WMS.Practice.Application.Queries.PersonQueries.EmployeeClasses
{
    public class GetEmployeeClassByIdQuery : IRequest<EmployeeClassDTO>
    {
        public string EmployeeClassId { get; set; }

        public GetEmployeeClassByIdQuery(string employeeClassId)
        {
            EmployeeClassId = employeeClassId;
        }
    }
}
