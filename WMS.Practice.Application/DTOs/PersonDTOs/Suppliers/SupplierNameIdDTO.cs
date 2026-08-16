namespace WMS.Practice.Application.DTOs.PersonDTOs.Suppliers
{
    public class SupplierNameIdDTO
    {
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }

        public SupplierNameIdDTO(string supplierId, string supplierName)
        {
            SupplierId = supplierId;
            SupplierName = supplierName;
        }
    }
}
