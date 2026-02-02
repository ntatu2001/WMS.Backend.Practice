namespace WMS.Practice.Application.Commands.PersonCommands.Customers
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public string CustomerId { get; set; }

        public DeleteCustomerCommand(string customerId)
        {
            CustomerId = customerId;
        }
    }
}
