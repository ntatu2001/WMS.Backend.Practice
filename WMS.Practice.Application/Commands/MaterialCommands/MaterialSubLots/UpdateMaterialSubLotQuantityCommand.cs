namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialSubLots
{
    public class UpdateMaterialSubLotQuantityCommand : IRequest<bool>
    {
        public string LotNumber { get; set; }
        public double RequestQuantity { get; set; }

        public UpdateMaterialSubLotQuantityCommand(string lotNumber, double requestQuantity)
        {
            LotNumber = lotNumber;
            RequestQuantity = requestQuantity;
        }
    }
}
