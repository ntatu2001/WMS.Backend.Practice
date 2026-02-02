namespace WMS.Practice.Application.Commands.PersonCommands.Suppliers
{
    public class DeleteSupplierCommand : IRequest<bool>
    {
        public string SupplierId { get; set; }

        public DeleteSupplierCommand(string supplierId)
        {
            SupplierId = supplierId; 
        }
    }
}
