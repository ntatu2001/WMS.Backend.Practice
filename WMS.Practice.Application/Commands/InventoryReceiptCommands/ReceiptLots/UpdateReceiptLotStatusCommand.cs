namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.ReceiptLots
{
    public class UpdateReceiptLotStatusCommand : IRequest<bool>
    {
        public string ReceiptLotId { get; set; }
        public string ReceiptLotStatus { get; set; }

        public UpdateReceiptLotStatusCommand(string receiptLotId, string receiptLotStatus)
        {
            ReceiptLotId = receiptLotId;
            ReceiptLotStatus = receiptLotStatus;
        }
    }
}
