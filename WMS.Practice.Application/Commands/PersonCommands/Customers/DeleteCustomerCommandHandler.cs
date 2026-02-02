namespace WMS.Practice.Application.Commands.PersonCommands.Customers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var existingCustomer = await _customerRepository.GetCustomerByCustomerIdAsync(request.CustomerId)
                                ?? throw new EntityNotFoundException("Customer could not found", nameof(request.CustomerId));

            _customerRepository.Remove(existingCustomer);
            return await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
