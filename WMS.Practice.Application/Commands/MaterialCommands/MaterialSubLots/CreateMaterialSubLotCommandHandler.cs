namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialSubLots
{
    public class CreateMaterialSubLotCommandHandler : IRequestHandler<CreateMaterialSubLotCommand, bool>
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;

        public CreateMaterialSubLotCommandHandler(IMaterialSubLotRepository materialSubLotRepository)
        {
            _materialSubLotRepository = materialSubLotRepository;
        }

        public async Task<bool> Handle(CreateMaterialSubLotCommand request, CancellationToken cancellationToken)
        {
            if (await _materialSubLotRepository.ExistsAsync(request.SubLotId))
            {
                throw new DuplicateRecordException(nameof(MaterialSubLot), request.SubLotId);
            }

            var newMaterialSubLot = new MaterialSubLot(materialSubLotId: request.SubLotId,
                                                       subLotStatus: request.SubLotStatus.ParseEnum<LotStatus>(),
                                                       existingQuantity: request.ExistingQuantity,
                                                       unitOfMeasure: request.UnitOfMeasure.ParseEnum<UnitOfMeasure>(),
                                                       locationId: request.LocationId,
                                                       lotNumber: request.LotNumber);

            _materialSubLotRepository.Create(newMaterialSubLot);
            return await _materialSubLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
