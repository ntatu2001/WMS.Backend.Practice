namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetMaterialsByWarehouseIdQueryHandler : IRequestHandler<GetMaterialsByWarehouseIdQuery, IEnumerable<MaterialByWarehouseIdDTO>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetMaterialsByWarehouseIdQueryHandler(IWarehouseRepository warehouseRepository, IMaterialRepository materialRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialByWarehouseIdDTO>> Handle(GetMaterialsByWarehouseIdQuery request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(request.WarehouseId)
                         ?? throw new EntityNotFoundException($"Warehouse with id {request.WarehouseId} could not found");

            var materialClassId = warehouse.GetMaterialClassId();
            if (string.IsNullOrEmpty(materialClassId)) 
                throw new InvalidDataException("Material class id is null or empty for the warehouse");

            var materials = await _materialRepository.GetMaterialsByClassIdAsync(materialClassId)
                         ?? throw new EntityNotFoundException($"Material could not found with Class Id {materialClassId}");

            return _mapper.Map<IEnumerable<MaterialByWarehouseIdDTO>>(materials);
        }
    }
}
