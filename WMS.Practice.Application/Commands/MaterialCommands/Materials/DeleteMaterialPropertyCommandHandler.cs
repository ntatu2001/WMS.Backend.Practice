namespace WMS.Practice.Application.Commands.MaterialCommands.Materials
{
    public class DeleteMaterialPropertyCommandHandler : IRequestHandler<DeleteMaterialPropertyCommand, bool>
    {
        private readonly IMaterialPropertyRepository _materialPropertyRepository;
        public DeleteMaterialPropertyCommandHandler(IMaterialPropertyRepository materialPropertyRepository)
        {
            _materialPropertyRepository = materialPropertyRepository;
        }

        public async Task<bool> Handle(DeleteMaterialPropertyCommand request, CancellationToken cancellationToken)
        {
            var existingProperty = await _materialPropertyRepository.GetMaterialPropertyByPropertyIdAsync(request.PropertyId)
                                ?? throw new EntityNotFoundException("Material Property could not found", nameof(request.PropertyId));

            _materialPropertyRepository.Delete(existingProperty);
            return await _materialPropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
