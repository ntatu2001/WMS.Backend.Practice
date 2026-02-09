namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceipts
{
    public class CreateInventoryReceiptCommandHandler : IRequestHandler<CreateInventoryReceiptCommand, string>
    {
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IReceiptLotRepository _receiptLotRepository;

        public CreateInventoryReceiptCommandHandler(IInventoryReceiptRepository inventoryReceiptRepository, ISupplierRepository supplierRepository, 
                                                    IEmployeeRepository personRepository, IWarehouseRepository warehouseRepository, 
                                                    IMaterialRepository materialRepository, IReceiptLotRepository receiptLotRepository)
        {
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _supplierRepository = supplierRepository;
            _employeeRepository = personRepository;
            _warehouseRepository = warehouseRepository;
            _materialRepository = materialRepository;
            _receiptLotRepository = receiptLotRepository;
        }

        public async Task<string> Handle(CreateInventoryReceiptCommand request, CancellationToken cancellationToken)
        {
            if (await _supplierRepository.ExistsAsync(request.SupplierId) is false)
                throw new EntityNotFoundException(nameof(Supplier), request.SupplierId);

            if (await _employeeRepository.ExistAsync(request.EmployeeId) is false)
                throw new EntityNotFoundException(nameof(Employee), request.EmployeeId);

            if (await _warehouseRepository.ExistsAsync(request.WarehouseId) is false)
                throw new EntityNotFoundException(nameof(Warehouse), request.WarehouseId);

            var newInventoryReceipt = CreateNewInventoryReceipt(request);

            foreach (var entry in request.Entries)
            {
                var newEntry = await CreateInventoryReceiptEntry(entry, newInventoryReceipt.InventoryReceiptId);
                newInventoryReceipt.UpdateReceiptEntry(newEntry);
            }

            _inventoryReceiptRepository.Create(newInventoryReceipt);

            await _inventoryReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return newInventoryReceipt.InventoryReceiptId;

        }

        private InventoryReceipt CreateNewInventoryReceipt(CreateInventoryReceiptCommand request) => new InventoryReceipt(inventoryReceiptId: Guid.NewGuid().ToString(),
                                                                                                                          receiptDate: request.ReceiptDate ?? DateTime.Now.ToVietNamTime(),
                                                                                                                          receiptStatus: ReceiptStatus.Pending,
                                                                                                                          supplierId: request.SupplierId,
                                                                                                                          employeeId: request.EmployeeId,
                                                                                                                          warehouseId: request.WarehouseId);

        private async Task<InventoryReceiptEntry> CreateInventoryReceiptEntry(CreateReceiptEntryCommand request, string InventoryReceiptId)
        {
            var material = await _materialRepository.GetByMaterialIdAsync(request.MaterialId)
                        ?? throw new EntityNotFoundException(nameof(Material), request.MaterialId);

            if (await _receiptLotRepository.ExistAsync(request.LotNumber) is true)
                throw new DuplicateRecordException(nameof(ReceiptLot), request.LotNumber);

            var newEntry = new InventoryReceiptEntry(inventoryReceiptEntryId: Guid.NewGuid().ToString(),
                                                     purchaseOrderNumber: request.PurchaseOrderNumber ?? request.LotNumber,
                                                     materialId: request.MaterialId,
                                                     materialName: request.MaterialName ?? material.MaterialName,
                                                     note: request.Note ?? "None",
                                                     importedQuantity: request.ImportedQuantity ?? 0,
                                                     lotNumber: request.LotNumber,
                                                     inventoryReceiptId: InventoryReceiptId);

            var newReceiptLot = new ReceiptLot(receiptLotId: newEntry.LotNumber,
                                               importedQuantity: newEntry.ImportedQuantity,
                                               lotStatus: LotStatus.Pending,
                                               inventoryReceiptEntryId: newEntry.InventoryReceiptEntryId);

            newEntry.UpdateReceiptLot(newReceiptLot);
            return newEntry;
        }
    }
}
