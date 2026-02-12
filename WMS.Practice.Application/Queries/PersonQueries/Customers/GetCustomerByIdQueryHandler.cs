namespace WMS.Practice.Application.Queries.PersonQueries.Customers
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper; 
        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId)
                                ?? throw new EntityNotFoundException($"Customer {request.CustomerId} could not found", nameof(request.CustomerId));

            var customerDTO = _mapper.Map<CustomerDTO>(existingCustomer);
            return customerDTO;
        }
    }
}
