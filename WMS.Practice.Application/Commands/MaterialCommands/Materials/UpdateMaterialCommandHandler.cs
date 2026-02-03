namespace WMS.Practice.Application.Commands.MaterialCommands.Materials
{
    public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand, bool>
    {
        private readonly IMaterialRepository _materialRepository;   
        public UpdateMaterialCommandHandler(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<bool> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            var existingMaterial = await _materialRepository.GetByMaterialIdAsync(request.MaterialId)
                                ?? throw new EntityNotFoundException("Material could not found", nameof(request.MaterialId));

            existingMaterial.UpdateMaterialInfo(request.MaterialName, request.MaterialClassId);
            foreach (var property in request.Properties)
            {
                if (existingMaterial.TryUpdateProperty(property.PropertyName, property.PropertyValue) is false)
                {
                    throw new ArgumentException($"Material Property Name {property.PropertyName} could not updated", nameof(property.PropertyName));
                }
            }

            _materialRepository.Update(existingMaterial);
            return await _materialRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
