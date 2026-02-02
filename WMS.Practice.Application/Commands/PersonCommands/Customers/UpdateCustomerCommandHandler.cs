namespace WMS.Practice.Application.Commands.PersonCommands.Customers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var existingCustomer = await _customerRepository.GetCustomerByCustomerIdAsync(request.CustomerId)
                                ?? throw new EntityNotFoundException("Customer could not found", nameof(request.CustomerId));

            existingCustomer.UpdateCustomerInfo(customerName: request.CustomerName,
                                                address: request.Address,
                                                contactDetails: request.ContactDetails);

            _customerRepository.Update(existingCustomer);
            return await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
