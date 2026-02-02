namespace WMS.Practice.Domain.AggregateModels.PersonAggregate
{
    public class Customer : Entity, IAggregateModel
    {
        public string CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string Address { get; private set; }
        public string ContactDetails { get; private set; }
        public List<InventoryIssue> InventoryIssues { get; private set; }

        public Customer(string customerId, string customerName, string address, string contactDetails)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            Address = address;
            ContactDetails = contactDetails;
        }

        public void UpdateCustomerInfo(string? customerName, string? address, string? contactDetails)
        {
            CustomerName = customerName ?? CustomerName;
            Address = address ?? Address;
            ContactDetails = contactDetails ?? ContactDetails;
        }
    }
}
