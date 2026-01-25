namespace WMS.Practice.Domain.AggregateModels.PersonAggregate
{
    public class Supplier : Entity, IAggregateModel
    {
        public string SupplierId { get; private set; }
        public string SupplierName { get; private set; }
        public string Address { get; private set; }
        public string ContactDetails { get; private set; }
        public Supplier(string supplierId, string supplierName, string address, string contactDetails)
        {
            SupplierId = supplierId;
            SupplierName = supplierName;
            Address = address;
            ContactDetails = contactDetails;
        }
    }
}
