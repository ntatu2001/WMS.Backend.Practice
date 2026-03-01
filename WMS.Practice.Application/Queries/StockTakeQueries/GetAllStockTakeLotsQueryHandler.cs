using WMS.Practice.Application.DTOs.StockTakeDTOs;

namespace WMS.Practice.Application.Queries.StockTakeQueries
{
    public class GetAllStockTakeLotsQueryHandler : IRequestHandler<GetAllStockTakeLotsQuery, IEnumerable<StockTakeLotDTO>>
    {
        private readonly IStockTakeRepository _stockTakeRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllStockTakeLotsQueryHandler(IStockTakeRepository stockTakeRepository, IWarehouseRepository warehouseRepository, 
                                               IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _stockTakeRepository = stockTakeRepository;
            _warehouseRepository = warehouseRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockTakeLotDTO>> Handle(GetAllStockTakeLotsQuery request, CancellationToken cancellationToken)
        {
            var stockTakeLots = await _stockTakeRepository.GetAllStockTakeLotsAsync()
                             ?? throw new EntityNotFoundException("Stock Take Lots could not found");

            var stockTakeLotDTOs = new List<StockTakeLotDTO>();
            foreach (var stockTakeLot in stockTakeLots)
            {
                var employeeName = await _employeeRepository.GetEmployeeNameByIdAsync(stockTakeLot.EmployeeId)
                                ?? throw new EntityNotFoundException($"Employee Name with Id {stockTakeLot.EmployeeId} could not found");

                var warehouseName = await _warehouseRepository.GetWarehouseNameByIdAsync(stockTakeLot.WarehouseId)
                                 ?? throw new EntityNotFoundException($"Warehouse Name with Id {stockTakeLot.WarehouseId} could not found");

                var stockTakeLotDTO = new StockTakeLotDTO(stockTakeId: stockTakeLot.StockTakeId,
                                                          adjustmentDate: stockTakeLot.AdjustmentDate,
                                                          previousQuantity: stockTakeLot.PreviousQuantity,
                                                          adjustedQuantity: stockTakeLot.AdjustedQuantity,
                                                          note: stockTakeLot.Note,
                                                          lotNumber: stockTakeLot.LotNumber,
                                                          warehouseId: stockTakeLot.WarehouseId,
                                                          warehouseName: warehouseName,
                                                          personId: stockTakeLot.EmployeeId,
                                                          personName: employeeName,
                                                          adjustmentType: stockTakeLot.GetAdjustmentTypeString(),
                                                          adjustmentReason: stockTakeLot.GetAdjustmentReasonString(),
                                                          adjustmentStatus: stockTakeLot.GetAdjustmentStatusString());

                stockTakeLotDTO.MapName(warehouseName, employeeName);
                var stockTakeSubLotDTOs = _mapper.Map<List<StockTakeSubLotDTO>>(stockTakeLot.SubLots);
                if (stockTakeSubLotDTOs?.Count > 0)
                    stockTakeLotDTO.StockTakeSubLots = stockTakeSubLotDTOs;

                stockTakeLotDTOs.Add(stockTakeLotDTO);
            }

            return stockTakeLotDTOs;
        }
    }
}
