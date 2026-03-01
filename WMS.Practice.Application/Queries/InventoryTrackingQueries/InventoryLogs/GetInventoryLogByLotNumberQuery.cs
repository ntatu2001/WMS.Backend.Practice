namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.InventoryLogs
{
    public class GetInventoryLogByLotNumberQuery : IRequest<IEnumerable<InventoryLogDTO>>
    {
        public string LotNumber { get; set; }
        public string Status { get; set; }

        public GetInventoryLogByLotNumberQuery(string lotNumber, string status)
        {
            LotNumber = lotNumber;
            Status = status;
        }
    }
}
