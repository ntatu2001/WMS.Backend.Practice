namespace WMS.Practice.Application.Commands.PersonCommands.Suppliers
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, bool>
    {
        private readonly ISupplierRepository _supplierRepository;
        public CreateSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<bool> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            if (await _supplierRepository.ExistsAsync(request.SupplierId) is true)
            {
                throw new DuplicateRecordException("Supplier is duplicated", nameof(request.SupplierId));
            }

            var newSupplier = new Supplier(supplierId: request.SupplierId,
                                           supplierName: request.SupplierName,
                                           address: request.Address,
                                           contactDetails: request.ContactDetails);

            _supplierRepository.Create(newSupplier);
            return await _supplierRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
