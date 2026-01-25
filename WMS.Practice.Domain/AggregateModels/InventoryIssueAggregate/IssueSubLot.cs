namespace WMS.Practice.Domain.AggregateModels.InventoryIssueAggregate
{
    public class IssueSubLot : Entity, IAggregateModel
    {
        public string IssueSubLotId { get; set; }
        public double RequestedQuantity { get; set; }
        public string MaterialSubLotId { get; set; }
        public MaterialSubLot MaterialSubLot { get; set; }
        public string IssueLotId { get; set; }
        public IssueLot IssueLot { get; set; }

        public IssueSubLot(string issueSubLotId, double requestedQuantity, string materialSubLotId, string issueLotId)
        {
            if (string.IsNullOrWhiteSpace(issueSubLotId))
                throw new ArgumentNullException(nameof(issueSubLotId));

            if (requestedQuantity < 0)
                throw new ArgumentException("Requested quantity cannot be negative.", nameof(requestedQuantity));

            IssueSubLotId = issueSubLotId;
            RequestedQuantity = requestedQuantity;
            MaterialSubLotId = materialSubLotId;
            IssueLotId = issueLotId;
        }
    }
}
