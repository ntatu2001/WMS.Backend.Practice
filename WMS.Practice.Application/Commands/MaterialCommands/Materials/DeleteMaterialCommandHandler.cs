namespace WMS.Practice.Application.Commands.MaterialCommands.Materials
{
    public class DeleteMaterialCommandHandler : IRequestHandler<DeleteMaterialCommand, bool>
    {
        private readonly IMaterialRepository _materialRepository;
        public DeleteMaterialCommandHandler(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<bool> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
        {
            var existingMaterial = await _materialRepository.GetMaterialByIdAsync(request.MaterialId)
                                ?? throw new EntityNotFoundException("Material could not found", nameof(request.MaterialId));

            _materialRepository.Delete(existingMaterial);
            return await _materialRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
