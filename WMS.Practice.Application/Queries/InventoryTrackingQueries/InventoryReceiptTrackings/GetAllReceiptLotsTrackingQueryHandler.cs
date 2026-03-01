using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Practice.Infrastructure.Repositories.PersonRepositories;

namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.InventoryReceiptTrackings
{
    public class GetAllReceiptLotsTrackingQueryHandler : IRequestHandler<GetAllReceiptLotsTrackingQuery, List<ReceiptLotsTrackingDTO>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IReceiptLotRepository _receiptLotRepository;
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IReceiptSubLotRepository _receiptSubLotRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetAllReceiptLotsTrackingQueryHandler(IWarehouseRepository warehouseRepository, IEmployeeRepository employeeRepository, ISupplierRepository supplierRepository, IReceiptLotRepository receiptLotRepository, IInventoryReceiptRepository inventoryReceiptRepository, IReceiptSubLotRepository receiptSubLotRepository, IMaterialRepository materialRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _employeeRepository = employeeRepository;
            _supplierRepository = supplierRepository;
            _receiptLotRepository = receiptLotRepository;
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _receiptSubLotRepository = receiptSubLotRepository;
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<List<ReceiptLotsTrackingDTO>> Handle(GetAllReceiptLotsTrackingQuery request, CancellationToken cancellationToken)
        {
            var inventoryReceiptsQuery = _inventoryReceiptRepository.QueryInventoryReceipts();

            if (request.SupplierId is not null)
            {
                inventoryReceiptsQuery = inventoryReceiptsQuery.Where(s => s.SupplierId == request.SupplierId);
            }

            if (request.StartTime is not null && request.EndTime is not null)
            {
                inventoryReceiptsQuery = inventoryReceiptsQuery.Where(s => s.ReceiptDate >= request.StartTime && s.ReceiptDate <= request.EndTime);
            }

            if (request.LotNumber is not null)
            {
                inventoryReceiptsQuery = inventoryReceiptsQuery.Where(r => r.Entries.Any(e => e.LotNumber == request.LotNumber));
            }

            inventoryReceiptsQuery = inventoryReceiptsQuery.Where(r => r.Entries.Any());

            var inventoryReceipts = await inventoryReceiptsQuery.ToListAsync();
            if (inventoryReceipts.Count == 0)
            {
                throw new EntityNotFoundException("Receipts Not Found");
            }

            var inventoryReceiptTrackingDTOs = new List<ReceiptLotsTrackingDTO>();
            foreach (var inventoryReceipt in inventoryReceipts)
            {
                var (warehouseId, warehouseName) = await _warehouseRepository.GetWarehouseNameAndIdByIdAsync(inventoryReceipt.WarehouseId)
                                                ?? throw new EntityNotFoundException($"Warehouse Name and Id could not found with {inventoryReceipt.WarehouseId}");

                var (supplierId, supplierName) = await _supplierRepository.GetSupplierNameAndIdByIdAsync(inventoryReceipt.SupplierId)
                                              ?? throw new EntityNotFoundException($"Customer Id and Name could not found with {inventoryReceipt.SupplierId}");

                var (employeeId, employeeName) = await _employeeRepository.GetEmployeeIdAndNameByIdAsync(inventoryReceipt.EmployeeId)
                                              ?? throw new EntityNotFoundException($"Employee Id and Name could not found with {inventoryReceipt.EmployeeId}");

                foreach (var entry in inventoryReceipt.Entries)
                {
                    var material = await _materialRepository.GetMaterialByIdAsync(entry.MaterialId)
                                ?? throw new EntityNotFoundException($"Material with Id {entry.MaterialId} could not found");

                    if (material.TryGetUnitOfMeasure(out string unitOfMeasure) is false)
                    {
                        throw new EntityNotFoundException("Unit Of Measure Not Found");
                    }

                    var receiptSublotDTOs = _mapper.Map<List<ReceiptSubLotDTO>>(entry.ReceiptLot.ReceiptSubLots);

                    var InventoryReceiptTrackingDTO = new ReceiptLotsTrackingDTO(warehouseID: warehouseId,
                                                                                 warehouseName: warehouseName,
                                                                                 personId: employeeId,
                                                                                 personName: employeeName,
                                                                                 supplierId: supplierId,
                                                                                 supplierName: supplierName,
                                                                                 receiptDate: inventoryReceipt.ReceiptDate,
                                                                                 lotNumber: entry.LotNumber,
                                                                                 importedQuantity: entry.ImportedQuantity,
                                                                                 materialId: material.MaterialId,
                                                                                 materialName: material.MaterialName,
                                                                                 unitOfMeasure: unitOfMeasure,
                                                                                 lotStatus: entry.ReceiptLot.LotStatus,
                                                                                 receiptSubLots: receiptSublotDTOs);

                    inventoryReceiptTrackingDTOs.Add(InventoryReceiptTrackingDTO);
                }
            }

            if (inventoryReceiptTrackingDTOs.Count == 0)
            {
                throw new EntityNotFoundException("Receipt Lots Not Found");
            }

            return inventoryReceiptTrackingDTOs;
        }
    }
}
