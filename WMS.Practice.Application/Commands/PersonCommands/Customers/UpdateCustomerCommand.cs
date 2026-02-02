namespace WMS.Practice.Application.Commands.PersonCommands.Customers
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public string CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? ContactDetails { get; set; }

        public UpdateCustomerCommand(string customerId, string customerName, string address, string contactDetails)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            Address = address;
            ContactDetails = contactDetails;
        }
    }
}
