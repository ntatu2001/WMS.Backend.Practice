namespace WMS.Practice.Application.Queries.PersonQueries.Customers
{
    public class GetCustomerByIdQuery : IRequest<CustomerDTO>
    {
        public string CustomerId { get; set; }

        public GetCustomerByIdQuery(string customerId)
        {
            CustomerId = customerId;
        }
    }
}
