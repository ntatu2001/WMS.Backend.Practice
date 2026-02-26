namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetMaterialsByWarehouseIdAndMaterialLotQueryHandler : IRequestHandler<GetMaterialsByWarehouseIdAndMaterialLotQuery, IEnumerable<MaterialByWarehouseIdDTO>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;
        public GetMaterialsByWarehouseIdAndMaterialLotQueryHandler(IMaterialRepository materialRepository, IMapper mapper, IWarehouseRepository warehouseRepository)
        {
            _materialRepository = materialRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;  
        }
        public async Task<IEnumerable<MaterialByWarehouseIdDTO>> Handle(GetMaterialsByWarehouseIdAndMaterialLotQuery request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(request.WarehouseId)
                         ?? throw new EntityNotFoundException($"Warehouse could not found for the given id {request.WarehouseId}");

            var materialClassId = warehouse.GetMaterialClassId();
            if (string.IsNullOrEmpty(materialClassId))
                throw new EntityNotFoundException($"Material Class Id could not parsed from the given warehouse id {request.WarehouseId}");

            var materials = await _materialRepository.GetMaterialsByClassIdAndMaterialLots(materialClassId)
                         ?? throw new EntityNotFoundException($"Materials could not found for the given material class id {materialClassId}");

            return _mapper.Map<IEnumerable<MaterialByWarehouseIdDTO>>(materials);
         }
    }
}
