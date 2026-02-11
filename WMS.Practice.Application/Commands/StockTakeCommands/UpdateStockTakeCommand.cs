namespace WMS.Practice.Application.Commands.StockTakeCommands
{
    public class UpdateStockTakeCommand : IRequest<bool>
    {
        public List<MaterialSubLotsCommand> MaterialSubLots { get; set; }
        public string LotNumber { get; set; }
        public string StockTakeId { get; set; }

        public UpdateStockTakeCommand(List<MaterialSubLotsCommand> materialSubLots, string lotNumber, string stockTakeId)
        {
            MaterialSubLots = materialSubLots;
            LotNumber = lotNumber;
            StockTakeId = stockTakeId;
        }
    }
}
