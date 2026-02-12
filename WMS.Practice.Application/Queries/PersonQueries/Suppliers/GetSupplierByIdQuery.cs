namespace WMS.Practice.Application.Queries.PersonQueries.Suppliers
{
    public class GetSupplierByIdQuery : IRequest<SupplierDTO>
    {
        public string SupplierId { get; set; }

        public GetSupplierByIdQuery(string supplierId)
        {
            SupplierId = supplierId;
        }
    }
}
