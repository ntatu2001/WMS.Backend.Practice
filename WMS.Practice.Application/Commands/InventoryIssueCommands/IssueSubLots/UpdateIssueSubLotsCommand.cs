namespace WMS.Practice.Application.Commands.InventoryIssueCommands.IssueSubLots
{
    public class UpdateIssueSubLotsCommand : IRequest<bool>
    {
        public List<IssueSubLotNewDTO> IssueSubLots { get; set; }

        public UpdateIssueSubLotsCommand(List<IssueSubLotNewDTO> issueSubLots)
        {
            IssueSubLots = issueSubLots;
        }
    }

    public class IssueSubLotNewDTO
    {
        public string IssueSublotId { get; set; }
        public string IssueLotId { get; set; }
        public double RequestedQuantity { get; set; }
        public string MaterialSubLotId { get; set; }
        public string LotNumber { get; set; }

        public IssueSubLotNewDTO(string issueSublotId, string issueLotId, double requestedQuantity, string materialSubLotId, string lotNumber)
        {
            IssueSublotId = issueSublotId;
            IssueLotId = issueLotId;
            RequestedQuantity = requestedQuantity;
            MaterialSubLotId = materialSubLotId;
            LotNumber = lotNumber;
        }
    }
}
