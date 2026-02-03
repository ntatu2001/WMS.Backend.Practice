using WMS.Practice.Domain.AggregateModels.MaterialAggregate;

namespace WMS.Practice.Application.Commands.MaterialCommands.Materials
{
    public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, bool>
    {
        private readonly IMaterialRepository _materialRepository;
        public CreateMaterialCommandHandler(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<bool> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
        {
            if (await _materialRepository.ExistAsync(request.MaterialId) is true)
            {
                throw new DuplicateRecordException("Material is duplicated", nameof(request.MaterialId));
            }

            var newMaterial = new Material(materialId: request.MaterialId,
                                           materialName: request.MaterialName,
                                           materialClassId: request.MaterialClassId);

            _materialRepository.Create(newMaterial);
            return await _materialRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
