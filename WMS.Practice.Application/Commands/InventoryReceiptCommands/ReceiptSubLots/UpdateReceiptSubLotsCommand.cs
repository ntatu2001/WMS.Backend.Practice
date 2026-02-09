namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.ReceiptSubLots
{
    public class UpdateReceiptSubLotsCommand : IRequest<bool>
    {
        public List<ReceiptSubLotNewDTO> ReceiptSubLots { get; set; }

        public UpdateReceiptSubLotsCommand(List<ReceiptSubLotNewDTO> receiptSubLots)
        {
            ReceiptSubLots = receiptSubLots;
        }
    }

    public class ReceiptSubLotNewDTO
    {
        public string ReceiptSubLotId { get; set; }
        public string MaterialId { get; set; }
        public double ImportedQuantity { get; set; }
        public string LocationId { get; set; }
        public string LotNumber { get; set; }

        public ReceiptSubLotNewDTO(string receiptSubLotId, string materialId, double importedQuantity, string locationId, string lotNumber)
        {
            ReceiptSubLotId = receiptSubLotId;
            MaterialId = materialId;
            ImportedQuantity = importedQuantity;
            LocationId = locationId;
            LotNumber = lotNumber;
        }
    }
}
