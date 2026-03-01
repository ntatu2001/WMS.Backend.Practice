using WMS.Practice.Application.DTOs.InventoryTrackingDTOs;

namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.InventoryIssueTrackings
{
    public class GetAllIssueLotsTrackingQuery : TimeRangeQuery, IRequest<List<IssueLotsTrackingDTO>>
    {
        public string? LotNumber { get; set; }
        public string? CustomerId { get; set; }

        public GetAllIssueLotsTrackingQuery(string? lotNumber, string? customerId, DateTime? startTime, DateTime? endTime)
        {
            LotNumber = lotNumber;
            CustomerId = customerId;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
