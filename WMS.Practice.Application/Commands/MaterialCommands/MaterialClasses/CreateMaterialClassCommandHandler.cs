namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialClasses
{
    public class CreateMaterialClassCommandHandler : IRequestHandler<CreateMaterialClassCommand, bool>
    {
        private readonly IMaterialClassRepository _materialClassRepository;
        public CreateMaterialClassCommandHandler(IMaterialClassRepository materialClassRepository)
        {
            _materialClassRepository = materialClassRepository;
        }

        public async Task<bool> Handle(CreateMaterialClassCommand request, CancellationToken cancellationToken)
        {
            if (await _materialClassRepository.ExistsAsync(request.MaterialClassId) is true)
            {
                throw new DuplicateRecordException("Material Class is duplicated", nameof(request.MaterialClassId));
            }

            var newMaterialClass = new MaterialClass(materialClassId: request.MaterialClassId,
                                                     materialClassName: request.ClassName);

            _materialClassRepository.Create(newMaterialClass);  
            return await _materialClassRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
