namespace WMS.Practice.Application.Commands.StockTakeCommands
{
    public class CreateStockTakeCommandHandler : IRequestHandler<CreateStockTakeCommand, string>
    {
        private readonly IStockTakeRepository _materialLotAdjustmentRepository;
        private readonly IMaterialLotRepository _materialLotRepository;

        public CreateStockTakeCommandHandler(IStockTakeRepository materialLotAdjustmentRepository, IMaterialLotRepository materialLotRepository)
        {
            _materialLotAdjustmentRepository = materialLotAdjustmentRepository;
            _materialLotRepository = materialLotRepository;
        }

        public async Task<string> Handle(CreateStockTakeCommand request, CancellationToken cancellationToken)
        {
            var materialLot = await _materialLotRepository.GetMaterialLotByIdAsync(request.LotNumber)
                           ?? throw new EntityNotFoundException(nameof(MaterialLot), request.LotNumber);

            var newStockTake = new StockTake(stockTakeId: Guid.NewGuid().ToString(),
                                             adjustmentDate: request.AdjustmentDate ?? DateTime.Now.ToVietNamTime(),
                                             previousQuantity: materialLot.ExistingQuantity,
                                             adjustedQuantity: 0,
                                             reason: request.Reason.ParseEnum<AdjustmentReason>(),
                                             status: AdjustmentStatus.Pending,
                                             type: request.AdjustmentType.ParseEnum<AdjustmentType>(),
                                             note: request.Note ?? "None",
                                             lotNumber: request.LotNumber,
                                             warehouseId: request.WarehouseId,
                                             employeeId: request.EmployeeId);

            if (await _materialLotAdjustmentRepository.ExistsAsync(newStockTake.StockTakeId) is true)
                throw new DuplicateRecordException(nameof(StockTake), newStockTake.StockTakeId);

            _materialLotAdjustmentRepository.Create(newStockTake);
            await _materialLotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return newStockTake.StockTakeId;
        }
    }
}
