namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.InventoryReceipts
{
    public class GetInventoryReceiptByIdQueryHandler : IRequestHandler<GetInventoryReceiptByIdQuery, InventoryReceiptDTO>
    {
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetInventoryReceiptByIdQueryHandler(IInventoryReceiptRepository inventoryReceiptRepository, IEmployeeRepository employeeRepository, ISupplierRepository supplierRepository, IWarehouseRepository warehouseRepository, IMapper mapper, IMediator mediator)
        {
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _employeeRepository = employeeRepository;
            _supplierRepository = supplierRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<InventoryReceiptDTO> Handle(GetInventoryReceiptByIdQuery request, CancellationToken cancellationToken)
        {
            var inventoryReceipt = await _inventoryReceiptRepository.GetInventoryReceiptByReceiptIdAsync(request.InventoryReceiptId)
                                ?? throw new EntityNotFoundException($"Inventory Receipt with Id {request.InventoryReceiptId} could not found");

            var employeeName = await _employeeRepository.GetEmployeeNameByIdAsync(inventoryReceipt.EmployeeId)
                            ?? throw new EntityNotFoundException($"Employee Name with Id {inventoryReceipt.EmployeeId} could not found");

            var supplierName = await _supplierRepository.GetSupplierNameByIdAsync(inventoryReceipt.SupplierId)
                            ?? throw new EntityNotFoundException($"Supplier Name with Id {inventoryReceipt.SupplierId} could not found");

            var warehouseName = await _warehouseRepository.GetWarehouseNameByIdAsync(inventoryReceipt.WarehouseId)
                             ?? throw new EntityNotFoundException($"Warehouse Name with Id {inventoryReceipt.WarehouseId} could not found");

            var entries = new List<InventoryReceiptEntryDTO>();

            foreach (var inventoryReceiptEntry in inventoryReceipt.Entries)
            {
                var inventoryReceiptEntryDTO = await _mediator.Send(new GetInventoryReceiptEntryByIdQuery(inventoryReceiptEntry.InventoryReceiptEntryId));
                entries.Add(inventoryReceiptEntryDTO);
            }

            var inventoryReceiptDTO = new InventoryReceiptDTO(inventoryReceiptId: inventoryReceipt.InventoryReceiptId,
                                                              receiptDate: inventoryReceipt.ReceiptDate,
                                                              receiptStatus: inventoryReceipt.ReceiptStatus,
                                                              supplierName: supplierName,
                                                              personName: employeeName,
                                                              warehouseName: warehouseName,
                                                              entries: entries);

            foreach (var entry in inventoryReceiptDTO.Entries)
            {
                entry.MapName(inventoryReceiptDTO.WarehouseName, inventoryReceiptDTO.SupplierName, inventoryReceiptDTO.PersonName);
            }

            return inventoryReceiptDTO;
        }
    }
}
