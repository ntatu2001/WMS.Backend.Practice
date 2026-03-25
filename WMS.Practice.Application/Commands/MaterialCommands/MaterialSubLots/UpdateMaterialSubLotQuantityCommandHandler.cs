namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialSubLots
{
    public class UpdateMaterialSubLotQuantityCommandHandler : IRequestHandler<UpdateMaterialSubLotQuantityCommand, bool>
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;

        public UpdateMaterialSubLotQuantityCommandHandler(IMaterialSubLotRepository materialSubLotRepository)
        {
            _materialSubLotRepository = materialSubLotRepository;
        }

        public async Task<bool> Handle(UpdateMaterialSubLotQuantityCommand request, CancellationToken cancellationToken)
        {
            var materialSubLot = await _materialSubLotRepository.GetMaterialSubLotByIdAsync(request.LotNumber)
                              ?? throw new EntityNotFoundException(nameof(MaterialSubLot), request.LotNumber);

            materialSubLot.UpdateExistingQuantity(existingQuantity: request.RequestQuantity);
            return await _materialSubLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
