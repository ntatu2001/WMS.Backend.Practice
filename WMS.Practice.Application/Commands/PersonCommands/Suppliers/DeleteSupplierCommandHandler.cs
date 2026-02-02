namespace WMS.Practice.Application.Commands.PersonCommands.Suppliers
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, bool>
    {
        private readonly ISupplierRepository _supplierRepository;
        public DeleteSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<bool> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var existingSupplier = await _supplierRepository.GetByIdAsync(request.SupplierId)
                                ?? throw new EntityNotFoundException("Supplier could not be found", nameof(request.SupplierId));

            _supplierRepository.Remove(existingSupplier);
            return await _supplierRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
