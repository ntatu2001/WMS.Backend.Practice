namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.InventoryReceipts
{
    public class GetAllInventoryReceiptsQueryHandler : IRequestHandler<GetAllInventoryReceiptsQuery, IEnumerable<InventoryReceiptDTO>>
    {
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetAllInventoryReceiptsQueryHandler(IInventoryReceiptRepository inventoryReceiptRepository, IEmployeeRepository employeeRepository, ISupplierRepository supplierRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _employeeRepository = employeeRepository;
            _supplierRepository = supplierRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryReceiptDTO>> Handle(GetAllInventoryReceiptsQuery request, CancellationToken cancellationToken)
        {
            var inventoryReceipts = await _inventoryReceiptRepository.GetAllInventoryReceiptsAsync()
                                 ?? throw new EntityNotFoundException("Inventory Receipts could not found");

            var inventoryReceiptDTOs = new List<InventoryReceiptDTO>();
            foreach (var inventoryReceipt in inventoryReceipts)
            {
                var inventoryReceiptDTO = _mapper.Map<InventoryReceiptDTO>(inventoryReceipt);

                var employeeName = await _employeeRepository.GetEmployeeNameByIdAsync(inventoryReceipt.EmployeeId)
                                ?? throw new EntityNotFoundException($"Employee Name with Id {inventoryReceipt.EmployeeId} could not found");

                var supplierName = await _supplierRepository.GetSupplierNameByIdAsync(inventoryReceipt.SupplierId)
                                ?? throw new EntityNotFoundException($"Supplier Name with Id {inventoryReceipt.SupplierId} could not found");

                var warehouseName = await _warehouseRepository.GetWarehouseNameByIdAsync(inventoryReceipt.WarehouseId)
                                 ?? throw new EntityNotFoundException($"Warehouse Name with Id {inventoryReceipt.WarehouseId} could not found");

                inventoryReceiptDTO.MapName(employeeName, warehouseName, supplierName);
                foreach (var entry in inventoryReceiptDTO.Entries)
                {
                    entry.MapName(inventoryReceiptDTO.WarehouseName, inventoryReceiptDTO.SupplierName, inventoryReceiptDTO.PersonName);
                }

                inventoryReceiptDTOs.Add(inventoryReceiptDTO);
            }

            return inventoryReceiptDTOs;
        }
    }
}
