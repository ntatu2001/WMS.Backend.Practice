namespace WMS.Practice.Domain.AggregateModels.PersonAggregate
{
    public class Supplier : Entity, IAggregateModel
    {
        #region Properties
        public string SupplierId { get; private set; }
        public string SupplierName { get; private set; }
        public string Address { get; private set; }
        public string ContactDetails { get; private set; }
        public List<InventoryReceipt> InventoryReceipts { get; private set; }

        #endregion

        #region Constructor

        public Supplier(string supplierId, string supplierName, string address, string contactDetails)
        {
            SupplierId = supplierId;
            SupplierName = supplierName;
            Address = address;
            ContactDetails = contactDetails;
        }

        #endregion

        #region Update Methods

        public void UpdateSupplierInfos(string? supplierName, string? address, string? contactDetails)
        {
            SupplierName = supplierName ?? SupplierName;
            Address = address ?? Address;
            ContactDetails = contactDetails ?? ContactDetails;
        }

        #endregion
    }
}
