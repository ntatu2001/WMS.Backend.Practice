namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialLots
{
    public class UpdateMaterialLotQuantityCommandHandler : IRequestHandler<UpdateMaterialLotQuantityCommand, bool>
    {
        private readonly IMaterialLotRepository _materialLotRepository;

        public UpdateMaterialLotQuantityCommandHandler(IMaterialLotRepository materialLotRepository)
        {
            _materialLotRepository = materialLotRepository;
        }

        public async Task<bool> Handle(UpdateMaterialLotQuantityCommand request, CancellationToken cancellationToken)
        {
            var materialLot = await _materialLotRepository.GetMaterialLotByIdAsync(request.MaterialLotId)
                           ?? throw new Exception("No Material Lots found.");

            // When Issue Lot is finished, the existing quantity of Material SubLots different from Existing Quantity of Material Lot.
            // We have to update Existing Quantity from Material SubLots to Material Lot
            double totalSubLotQuantity = materialLot.GetExistingQuantity();
            materialLot.Update(existingQuantity: totalSubLotQuantity);

            return await _materialLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
