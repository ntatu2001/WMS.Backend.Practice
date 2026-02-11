namespace WMS.Practice.Application.Commands.InventoryIssueCommands.IssueLots
{
    public class UpdateIssueLotStatusCommand : IRequest<bool>
    {
        public string IssueLotId { get; set; }
        public string IssueLotStatus { get; set; }

        public UpdateIssueLotStatusCommand(string issueLotId, string issueLotStatus)
        {
            IssueLotId = issueLotId;
            IssueLotStatus = issueLotStatus;
        }
    }
}
