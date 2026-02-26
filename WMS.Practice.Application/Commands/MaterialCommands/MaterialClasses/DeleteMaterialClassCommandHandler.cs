namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialClasses
{
    public class DeleteMaterialClassCommandHandler : IRequestHandler<DeleteMaterialClassCommand, bool>
    {
        private readonly IMaterialClassRepository _materialClassRepository;
        public DeleteMaterialClassCommandHandler(IMaterialClassRepository materialClassRepository)
        {
            _materialClassRepository = materialClassRepository;
        }

        public async Task<bool> Handle(DeleteMaterialClassCommand request, CancellationToken cancellationToken)
        {
            var existingClass = await _materialClassRepository.GetMaterialClassByClassIdAsync(request.MaterialClassId)
                             ?? throw new EntityNotFoundException("Material Class could not found", nameof(request.MaterialClassId));

            _materialClassRepository.Delete(existingClass);
            return await _materialClassRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
