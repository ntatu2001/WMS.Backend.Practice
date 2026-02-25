namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetLocationStorageHistoriesQueryHandler : IRequestHandler<GetLocationStorageHistoriesQuery, List<InventoryStorageTrackingDTO>>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IReceiptSubLotRepository _receiptSubLotRepository;
        private readonly IIssueSubLotRepository _issueSubLotRepository;

        public GetLocationStorageHistoriesQueryHandler(ILocationRepository locationRepository, IReceiptSubLotRepository receiptSubLotRepository, IIssueSubLotRepository issueSubLotRepository)
        {
            _locationRepository = locationRepository;
            _receiptSubLotRepository = receiptSubLotRepository;
            _issueSubLotRepository = issueSubLotRepository;
        }
        public async Task<List<InventoryStorageTrackingDTO>> Handle(GetLocationStorageHistoriesQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetLocationByIdAsync(request.LocationId)
                        ?? throw new EntityNotFoundException(nameof(Location), request.LocationId);

            var inventoryStorageTrackingDTOs = new List<InventoryStorageTrackingDTO>();

            // Get Start and End Time from Request, if they are Null, we will retrieve all receipts and issues
            var startTime = request.StartTime is not null ? request.StartTime.Value : DateTime.MinValue;
            var endTime = request.EndTime is not null ? request.EndTime.Value : DateTime.MaxValue;

            // Queries for Inventory Receipts and Issues of location in a specific time range.
            var receiptSubLotDateList = await _receiptSubLotRepository.GetReceiptSubLotsByLocationIdAndTimeRange(request.LocationId, startTime, endTime);
            var issueSubLotDateList = await _issueSubLotRepository.GetIssueSubLotsByLocationIdAndTimeRange(request.LocationId, startTime, endTime);

            foreach (var (receiptDate, receiptSubLot) in receiptSubLotDateList)
            {
                var materialName = await _receiptSubLotRepository.GetMaterialNameByReceiptSubLotIdAsync(receiptSubLot.ReceiptSubLotId)
                                ?? throw new InvalidDataException($"Material Name of Receipt SubLot {receiptSubLot.ReceiptSubLotId} could not be retrieved");

                var (outboundQuantity, issueDate) = GetOutBoundQuantityAndIssueDate(issueSubLotDateList, receiptSubLot.ReceiptSubLotId);
                var inventoryStorageTrackingDTO = new InventoryStorageTrackingDTO(materialName: materialName,
                                                                                  lotNumber: receiptSubLot.ReceiptSubLotId,
                                                                                  inboundQuantity: receiptSubLot.ImportedQuantity,
                                                                                  outboundQuantity: outboundQuantity,
                                                                                  availableQuantity: receiptSubLot.ImportedQuantity - outboundQuantity,
                                                                                  receiptDate: receiptDate,
                                                                                  issueDate: issueDate);

                inventoryStorageTrackingDTOs.Add(inventoryStorageTrackingDTO);
            }

            return inventoryStorageTrackingDTOs;
        }

        private (double OutBoundQuantity, DateTime IssueDate) GetOutBoundQuantityAndIssueDate(List<(DateTime IssueDate, IssueSubLot SubLot)> issueSubLotDateList, string materialSubLotId)
        {
            var subLotGroups = issueSubLotDateList.Where(x => x.SubLot.MaterialSubLot?.MaterialSubLotId == materialSubLotId)
                                                  .GroupBy(x => x.SubLot.MaterialSubLot?.MaterialSubLotId);

            double outboundQuantity = subLotGroups.Sum(group => group.Sum(x => x.SubLot.RequestedQuantity));
            DateTime issueDate = subLotGroups.Any() ? subLotGroups.Max(group => group.Max(x => x.IssueDate)) : DateTime.MinValue;
            return (outboundQuantity, issueDate);   
        }
    }
}
