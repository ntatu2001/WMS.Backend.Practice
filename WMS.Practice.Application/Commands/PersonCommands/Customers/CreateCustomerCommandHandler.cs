namespace WMS.Practice.Application.Commands.PersonCommands.Customers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (await _customerRepository.ExistsAsync(request.CustomerId) is true)
            {
                throw new DuplicateRecordException("Customer is duplicated", nameof(request.CustomerId));
            }

            var newCustomer = new Customer(customerId: request.CustomerId,
                                           customerName: request.CustomerName,
                                           address: request.Address,
                                           contactDetails: request.ContactDetails);

            _customerRepository.Create(newCustomer);
            return await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
