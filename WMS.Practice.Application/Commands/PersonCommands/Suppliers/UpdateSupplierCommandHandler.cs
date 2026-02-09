namespace WMS.Practice.Application.Commands.PersonCommands.Suppliers
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, bool>
    {
        private readonly ISupplierRepository _supplierRepository;
        public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<bool> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var existingSupplier = await _supplierRepository.GetSupplierByIdAsync(request.SupplierId)
                                ?? throw new EntityNotFoundException("Supplier could not be found", nameof(request.SupplierId));

            existingSupplier.UpdateSupplierInfos(supplierName: request.SupplierName,
                                                 address: request.Address,
                                                 contactDetails: request.ContactDetails);

            _supplierRepository.Update(existingSupplier);
            return await _supplierRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
