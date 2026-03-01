namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.StockTakeTrackings
{
    public class GetStockTakeLotTrackingQueryHandler : IRequestHandler<GetStockTakeLotTrackingQuery, List<StockTakeLotTrackingDTO>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStockTakeRepository _stockTakeRepository;
        private readonly IStockTakeSubLotRepository _stockTakeSubLotRepository;
        private readonly IMaterialRepository _materialRepository;

        public GetStockTakeLotTrackingQueryHandler(IWarehouseRepository warehouseRepository, IEmployeeRepository employeeRepository, 
                                                   IStockTakeRepository stockTakeRepository, IStockTakeSubLotRepository stockTakeSubLotRepository, 
                                                   IMaterialRepository materialRepository)
        {
            _warehouseRepository = warehouseRepository;
            _employeeRepository = employeeRepository;
            _stockTakeRepository = stockTakeRepository;
            _stockTakeSubLotRepository = stockTakeSubLotRepository;
            _materialRepository = materialRepository;
        }

        public async Task<List<StockTakeLotTrackingDTO>> Handle(GetStockTakeLotTrackingQuery request, CancellationToken cancellationToken)
        {
            var stockTakeLotsQuery = _stockTakeRepository.QueryAllStockTakes()
                                  ?? throw new EntityNotFoundException("Stock Take Lots could not found");

            if (request.StartTime is not null && request.EndTime is not null)
            {
                stockTakeLotsQuery = stockTakeLotsQuery.Where(s => s.AdjustmentDate >= request.StartTime && s.AdjustmentDate <= request.EndTime);
            }

            if (request.LotNumber is not null)
            {
                stockTakeLotsQuery = stockTakeLotsQuery.Where(r => r.LotNumber == request.LotNumber);
            }

            var stockTakeLots = await stockTakeLotsQuery.ToListAsync(cancellationToken);

            var stockTakeLotTrackingDTOs = new List<StockTakeLotTrackingDTO>();
            foreach (var stockTake in stockTakeLots)
            {
                var (warehouseId, warehouseName) = await _warehouseRepository.GetWarehouseNameAndIdByIdAsync(stockTake.WarehouseId)
                                                ?? throw new EntityNotFoundException($"Warehouse Name and Id could not found with {stockTake.WarehouseId}");

                var (employeeId, employeeName) = await _employeeRepository.GetEmployeeIdAndNameByIdAsync(stockTake.EmployeeId)
                                              ?? throw new EntityNotFoundException($"Employee Id and Name could not found with {stockTake.EmployeeId}");

                var material = await _materialRepository.GetMaterialByIdAsync(stockTake.MaterialLot.MaterialId)
                            ?? throw new EntityNotFoundException($"Material with Id {stockTake.MaterialLot.MaterialId} could not found");

                if (material.TryGetUnitOfMeasure(out string unitOfMeasure) is false)
                {
                    throw new EntityNotFoundException("Unit Of Measure Not Found");
                }

                var stockTakeSublotDTOs = new List<StockTakeSubLotDTO>();
                var stockTakeSublots = await _stockTakeSubLotRepository.GetStockTakeSubLotsByStockTakeId(stockTake.StockTakeId);
                if (stockTakeSublots.Count != 0)
                {
                    foreach (var sublot in stockTakeSublots)
                    {
                        var stockTakeSublotDTO = new StockTakeSubLotDTO(stockTakeSubLotId: sublot.StockTakeId,
                                                                        locationId: sublot.LocationId,
                                                                        materialSublotId: sublot.MaterialSubLotId,
                                                                        previousQuantity: sublot.PreviousQuantity,
                                                                        realAdjustmentQuantity: sublot.AdjustedQuantity,
                                                                        quantityDifference: sublot.QuantityDifference,
                                                                        stockTakeLotId: sublot.StockTakeId);
                        stockTakeSublotDTOs.Add(stockTakeSublotDTO);
                    }
                }

                if (stockTakeSublotDTOs.Count != 0)
                {
                    var LotAdjustmentsTrackingDTO = new StockTakeLotTrackingDTO(warehouseID: warehouseId,
                                                                                warehouseName: warehouseName,
                                                                                personId: employeeId,
                                                                                personName: employeeName,
                                                                                adjustmentDate: stockTake.AdjustmentDate,
                                                                                lotNumber: stockTake.LotNumber,
                                                                                materialId: material.MaterialId,
                                                                                materialName: material.MaterialName,
                                                                                unitOfMeasure: unitOfMeasure,
                                                                                lotStatus: stockTake.Status,
                                                                                stockTakeSublotDTOs: stockTakeSublotDTOs);

                    stockTakeLotTrackingDTOs.Add(LotAdjustmentsTrackingDTO);
                }
            }

            if (stockTakeLotTrackingDTOs.Count == 0)
            {
                throw new EntityNotFoundException("Stock Take Lots Could Not Found");
            }

            return stockTakeLotTrackingDTOs;
        }
    }
}
