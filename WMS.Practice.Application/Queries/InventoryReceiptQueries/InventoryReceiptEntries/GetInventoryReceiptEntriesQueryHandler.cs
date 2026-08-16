namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.InventoryReceiptEntries
{
    public class GetInventoryReceiptEntriesQueryHandler : IRequestHandler<GetInventoryReceiptEntriesQuery, IEnumerable<InventoryReceiptEntryDTO>>
    {
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IInventoryReceiptEntryRepository _inventoryReceiptEntryRepository;
        private readonly IReceiptLotRepository _receiptLotRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetInventoryReceiptEntriesQueryHandler(IInventoryReceiptRepository inventoryReceiptRepository, IInventoryReceiptEntryRepository inventoryReceiptEntryRepository,
                                                         IReceiptLotRepository receiptLotRepository, IMaterialRepository materialRepository, IWarehouseRepository warehouseRepository,
                                                         IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _inventoryReceiptEntryRepository = inventoryReceiptEntryRepository;
            _receiptLotRepository = receiptLotRepository;
            _materialRepository = materialRepository;
            _warehouseRepository = warehouseRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryReceiptEntryDTO>> Handle(GetInventoryReceiptEntriesQuery request, CancellationToken cancellationToken)
        {
            var inventoryReceiptEntries = await _inventoryReceiptEntryRepository.GetAllInventoryReceiptEntriesAsync()
                                       ?? throw new EntityNotFoundException("Inventory Receipt Entries could not found");

            var inventoryReceiptEntriesDTOs = new List<InventoryReceiptEntryDTO>();
            foreach (var inventoryReceiptEntry in inventoryReceiptEntries)
            {
                var inventoryReceipt = await _inventoryReceiptRepository.GetInventoryReceiptByReceiptIdAsync(inventoryReceiptEntry.InventoryReceiptId)
                                    ?? throw new EntityNotFoundException($"Inventory Receipt with Id {inventoryReceiptEntry.InventoryReceiptId} could not found");

                if (request.FromDate.HasValue && inventoryReceipt.ReceiptDate.Date < request.FromDate.Value.Date)
                    continue;

                if (request.ToDate.HasValue && inventoryReceipt.ReceiptDate.Date > request.ToDate.Value.Date)
                    continue;

                var receiptLot = await _receiptLotRepository.GetReceiptLotByIdAsync(inventoryReceiptEntry.LotNumber)
                              ?? throw new EntityNotFoundException($"Receipt Lot with Lot Number {inventoryReceiptEntry.LotNumber} could not found");

                var inventoryReceiptEntryDTO = _mapper.Map<InventoryReceiptEntryDTO>(inventoryReceiptEntry);
                inventoryReceiptEntryDTO.ReceiptLot = _mapper.Map<ReceiptLotDTO>(receiptLot);

                var material = await _materialRepository.GetMaterialByIdAsync(inventoryReceiptEntry.MaterialId)
                            ?? throw new EntityNotFoundException($"Material with Id {inventoryReceiptEntry.MaterialId} could not found");

                if (material.TryGetPropertyValue("Unit", out var unitValue))
                {
                    inventoryReceiptEntryDTO.Unit = unitValue;
                }


                var warehouseName = await _warehouseRepository.GetWarehouseNameByIdAsync(inventoryReceipt.WarehouseId)
                                 ?? throw new EntityNotFoundException($"Warehouse Name with Id {inventoryReceipt.WarehouseId} could not found");

                var employeeName = await _employeeRepository.GetEmployeeNameByIdAsync(inventoryReceipt.EmployeeId)
                                ?? throw new EntityNotFoundException($"Employee Name with Id {inventoryReceipt.EmployeeId} could not found");

                inventoryReceiptEntryDTO.MapName(material.MaterialName, warehouseName: warehouseName, personName: employeeName);
                inventoryReceiptEntryDTO.ReceiptDate = inventoryReceipt.ReceiptDate;
                inventoryReceiptEntriesDTOs.Add(inventoryReceiptEntryDTO);
            }

            return inventoryReceiptEntriesDTOs;
        }
    }
}
