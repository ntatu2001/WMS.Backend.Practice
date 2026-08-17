using WMS.Practice.Application.DTOs.InventoryTrackingDTOs;

namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.InventoryIssueTrackings
{
    public class GetAllIssueLotsTrackingQuery : TimeRangeQuery, IRequest<List<IssueLotsTrackingDTO>>
    {
        public string? LotNumber { get; set; }
        public string? CustomerName { get; set; }

        public GetAllIssueLotsTrackingQuery(string? lotNumber, string? customerName, DateTime? startTime, DateTime? endTime)
        {
            LotNumber = lotNumber;
            CustomerName = customerName;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
