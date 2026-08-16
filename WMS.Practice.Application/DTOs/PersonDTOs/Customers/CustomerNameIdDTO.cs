namespace WMS.Practice.Application.DTOs.PersonDTOs.Customers
{
    public class CustomerNameIdDTO
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public CustomerNameIdDTO(string customerId, string customerName)
        {
            CustomerId = customerId;
            CustomerName = customerName;
        }
    }
}
