namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialClasses
{
    public class UpdateMaterialClassCommandHandler : IRequestHandler<UpdateMaterialClassCommand, bool>
    {
        private readonly IMaterialClassRepository _materialClassRepository;
        public UpdateMaterialClassCommandHandler(IMaterialClassRepository materialClassRepository)
        {
            _materialClassRepository = materialClassRepository;
        }

        public async Task<bool> Handle(UpdateMaterialClassCommand request, CancellationToken cancellationToken)
        {
            var existingClass = await _materialClassRepository.GetByClassIdAsync(request.MaterialClassId)
                             ?? throw new EntityNotFoundException("Material Class could not found", nameof(request.MaterialClassId));

            existingClass.UpdateInfo(request.ClassName);
            foreach (var property in request.Properties)
            {
                if (existingClass.UpdateProperty(property.PropertyName, property.PropertyValue) is false)
                {
                    throw new ArgumentException($"Property Name {property.PropertyName} does not exist", nameof(property.PropertyName));
                }
            }

            _materialClassRepository.Update(existingClass);
            return await _materialClassRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
