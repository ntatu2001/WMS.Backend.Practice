namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.InventoryLogs
{
    public class GetAllInventoryLogsQuery : IRequest<IEnumerable<InventoryLogDTO>>
    {
        public string TransactionType { get; set; }

        public GetAllInventoryLogsQuery(string transactionType)
        {
            TransactionType = transactionType;
        }
    }
}
