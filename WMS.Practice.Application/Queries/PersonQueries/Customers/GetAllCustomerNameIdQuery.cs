using WMS.Practice.Application.DTOs.PersonDTOs.Customers;

namespace WMS.Practice.Application.Queries.PersonQueries.Customers
{
    public class GetAllCustomerNameIdQuery : IRequest<IEnumerable<CustomerNameIdDTO>>
    {
        public GetAllCustomerNameIdQuery() { }
    }
}
