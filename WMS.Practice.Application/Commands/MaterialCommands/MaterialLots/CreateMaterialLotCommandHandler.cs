namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialLots
{
    public class CreateMaterialLotCommandHandler : IRequestHandler<CreateMaterialLotCommand, bool>
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMaterialSubLotRepository _subLotRepository;
        public CreateMaterialLotCommandHandler(IMaterialLotRepository materialLotRepository, IMaterialSubLotRepository subLotRepository)
        {
            _materialLotRepository = materialLotRepository;
            _subLotRepository = subLotRepository;
        }

        public async Task<bool> Handle(CreateMaterialLotCommand request, CancellationToken cancellationToken)
        {
            if (await _materialLotRepository.ExistAsync(request.LotNumber) is true)
            {
                throw new DuplicateRecordException("Material Lot is duplicated", nameof(request.LotNumber));
            }

            var materialSubLots = new List<MaterialSubLot>();
            foreach (var subLot in request.SubLots)
            {
                if (await _subLotRepository.ExistsAsync(subLot.SubLotId) is true)
                {
                    throw new DuplicateRecordException("SubLot is duplicated", nameof(subLot.SubLotId));
                }

                var newSubLot = new MaterialSubLot(materialSubLotId: subLot.SubLotId,
                                                   subLotStatus: subLot.SubLotStatus.ParseEnum<LotStatus>(),
                                                   existingQuantity: subLot.ExistingQuantity,
                                                   locationId: subLot.LocationId,
                                                   lotNumber: request.LotNumber,
                                                   unitOfMeasure: subLot.UnitOfMeasure.ParseEnum<UnitOfMeasure>());
                materialSubLots.Add(newSubLot);
            }

            var newMaterialLot = new MaterialLot(lotNumber: request.LotNumber,
                                                 lotStatus: request.LotStatus.ParseEnum<LotStatus>(),
                                                 existingQuantity: request.ExisitingQuantity,
                                                 materialId: request.MaterialId);

            _materialLotRepository.Create(newMaterialLot);
            return await _materialLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
