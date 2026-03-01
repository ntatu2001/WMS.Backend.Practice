namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.InventoryReceiptEntries
{
    public class GetInventoryReceiptEntryByIdQueryHandler : IRequestHandler<GetInventoryReceiptEntryByIdQuery, InventoryReceiptEntryDTO>
    {
        private readonly IInventoryReceiptEntryRepository _inventoryReceiptEntryRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetInventoryReceiptEntryByIdQueryHandler(IInventoryReceiptEntryRepository inventoryReceiptEntryRepository,  IMaterialRepository materialRepository, 
                                                        IWarehouseRepository warehouseRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _inventoryReceiptEntryRepository = inventoryReceiptEntryRepository;
            _materialRepository = materialRepository;
            _warehouseRepository = warehouseRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<InventoryReceiptEntryDTO> Handle(GetInventoryReceiptEntryByIdQuery request, CancellationToken cancellationToken)
        {
            var inventoryReceiptEntry = await _inventoryReceiptEntryRepository.GetById(request.InventoryReceiptEntryId)
                                     ?? throw new EntityNotFoundException(nameof(InventoryReceiptEntry), request.InventoryReceiptEntryId);

            var inventoryReceiptEntryDTO = _mapper.Map<InventoryReceiptEntryDTO>(inventoryReceiptEntry);

            var materialName = await _materialRepository.GetMaterialNameByIdAsync(inventoryReceiptEntry.MaterialId)
                            ?? throw new EntityNotFoundException($"Material Name with Id {inventoryReceiptEntry.MaterialId} could not found");

            var warehouseName = await _warehouseRepository.GetWarehouseNameByIdAsync(inventoryReceiptEntry.InventoryReceipt.WarehouseId)
                             ?? throw new EntityNotFoundException($"Warehouse Name with Id {inventoryReceiptEntry.InventoryReceipt.WarehouseId} could not found");

            var employeeName = await _employeeRepository.GetEmployeeNameByIdAsync(inventoryReceiptEntry.InventoryReceipt.EmployeeId)
                            ?? throw new EntityNotFoundException($"Employee Name with Id {inventoryReceiptEntry.InventoryReceipt.EmployeeId} could not found");

            inventoryReceiptEntryDTO.MapName(materialName, warehouseName, employeeName);
            return inventoryReceiptEntryDTO;
        }
    }
}
