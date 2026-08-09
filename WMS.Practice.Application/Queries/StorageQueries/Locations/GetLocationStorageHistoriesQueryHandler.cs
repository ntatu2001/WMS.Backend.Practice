namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetLocationStorageHistoriesQueryHandler : IRequestHandler<GetLocationStorageHistoriesQuery, List<InventoryStorageTrackingDTO>>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IStockLocationHistoryRepository _stockLocationHistoryRepository;
        private readonly IMaterialSubLotRepository _materialSubLotRepository;

        public GetLocationStorageHistoriesQueryHandler(ILocationRepository locationRepository, IStockLocationHistoryRepository stockLocationHistoryRepository,
                                                        IMaterialSubLotRepository materialSubLotRepository)
        {
            _locationRepository = locationRepository;
            _stockLocationHistoryRepository = stockLocationHistoryRepository;
            _materialSubLotRepository = materialSubLotRepository;
        }

        public async Task<List<InventoryStorageTrackingDTO>> Handle(GetLocationStorageHistoriesQuery request, CancellationToken cancellationToken)
        {
            _ = await _locationRepository.GetLocationByIdAsync(request.LocationId)
                        ?? throw new EntityNotFoundException(nameof(Location), request.LocationId);

            // Get Start and End Time from Request, if they are Null, we will retrieve the full history
            var startTime = request.StartTime is not null ? request.StartTime.Value : DateTime.MinValue;
            var endTime = request.EndTime is not null ? request.EndTime.Value : DateTime.MaxValue;

            var histories = await _stockLocationHistoryRepository.GetByLocationIdAndTimeRangeAsync(request.LocationId, startTime, endTime);

            var inventoryStorageTrackingDTOs = new List<InventoryStorageTrackingDTO>();
            foreach (var group in histories.GroupBy(x => x.MaterialSubLotId))
            {
                var material = await _materialSubLotRepository.GetMaterialBySubLotIdAsync(group.Key)
                            ?? throw new EntityNotFoundException(nameof(Material), group.Key);

                var inboundEvents = group.Where(x => x.MovementType == StockMovementType.Inbound).ToList();
                var outboundEvents = group.Where(x => x.MovementType == StockMovementType.Outbound).ToList();

                var inboundQuantity = inboundEvents.Sum(x => x.Quantity);
                var outboundQuantity = outboundEvents.Sum(x => x.Quantity);
                var receiptDate = inboundEvents.Count > 0 ? inboundEvents.Min(x => x.EventDate) : group.Min(x => x.EventDate);
                var issueDate = outboundEvents.Count > 0 ? outboundEvents.Max(x => x.EventDate) : (DateTime?)null;

                inventoryStorageTrackingDTOs.Add(new InventoryStorageTrackingDTO(materialName: material.MaterialName,
                                                                                 lotNumber: group.First().LotNumber,
                                                                                 inboundQuantity: inboundQuantity,
                                                                                 outboundQuantity: outboundQuantity,
                                                                                 availableQuantity: inboundQuantity - outboundQuantity,
                                                                                 receiptDate: receiptDate,
                                                                                 issueDate: issueDate));
            }

            return inventoryStorageTrackingDTOs;
        }
    }
}
