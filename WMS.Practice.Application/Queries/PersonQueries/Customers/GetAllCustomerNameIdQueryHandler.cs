using WMS.Practice.Application.DTOs.PersonDTOs.Customers;

namespace WMS.Practice.Application.Queries.PersonQueries.Customers
{
    public class GetAllCustomerNameIdQueryHandler : IRequestHandler<GetAllCustomerNameIdQuery, IEnumerable<CustomerNameIdDTO>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomerNameIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerNameIdDTO>> Handle(GetAllCustomerNameIdQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllCustomerNameIdAsync();

            return customers.Select(c => new CustomerNameIdDTO(c.CustomerId, c.CustomerName));
        }
    }
}
