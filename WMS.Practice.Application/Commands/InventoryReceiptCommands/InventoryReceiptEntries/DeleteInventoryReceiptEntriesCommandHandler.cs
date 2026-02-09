namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceiptEntries
{
    public class DeleteInventoryReceiptEntriesCommandHandler : IRequestHandler<DeleteInventoryReceiptEntriesCommand, bool>
    {
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IInventoryReceiptEntryRepository _inventoryReceiptEntryRepository;

        public DeleteInventoryReceiptEntriesCommandHandler(IInventoryReceiptRepository inventoryReceiptRepository, IInventoryReceiptEntryRepository inventoryReceiptEntryRepository)
        {
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _inventoryReceiptEntryRepository = inventoryReceiptEntryRepository;
        }

        public async Task<bool> Handle(DeleteInventoryReceiptEntriesCommand request, CancellationToken cancellationToken)
        {
            var inventoryReceiptEntry = await _inventoryReceiptEntryRepository.GetById(request.EntryId)
                                     ?? throw new EntityNotFoundException("Inventory Receipt Entry could not found", nameof(request.EntryId));

            if (inventoryReceiptEntry.ReceiptLot.IsDone())
                throw new Exception($"Receipt lot {request.EntryId} is already done.");

            _inventoryReceiptEntryRepository.Delete(inventoryReceiptEntry);
            return await _inventoryReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
