namespace WMS.Practice.Application.Queries.StockTakeQueries
{
    public class GetStockTakeByIdQueryHandler : IRequestHandler<GetStockTakeByIdQuery, StockTakeLotDTO>
    {
        private readonly IStockTakeRepository _stockTakeRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetStockTakeByIdQueryHandler(IStockTakeRepository materialLotAdjustmentRepository, IWarehouseRepository warehouseRepository, 
                                            IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _stockTakeRepository = materialLotAdjustmentRepository;
            _warehouseRepository = warehouseRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<StockTakeLotDTO> Handle(GetStockTakeByIdQuery request, CancellationToken cancellationToken)
        {
            var stockTakeLot = await _stockTakeRepository.GetById(request.StockTakeId)
                            ?? throw new EntityNotFoundException($"Stock Take with Id {request.StockTakeId} could not found");

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

            return stockTakeLotDTO;
        }
    }
}
