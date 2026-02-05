namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialSubLots
{
    public class UpdateMaterialSubLotsCommand : IRequest<bool>
    {
        public List<MaterialSubLotsCommand> MaterialSubLots { get; set; }
        public string LotNumber { get; set; }
        public string MaterialLotAdjustmentId { get; set; }

        public UpdateMaterialSubLotsCommand(List<MaterialSubLotsCommand> materialSubLots, string lotNumber, string materialLotAdjustmentId)
        {
            MaterialSubLots = materialSubLots;
            LotNumber = lotNumber;
            MaterialLotAdjustmentId = materialLotAdjustmentId;
        }
    }

    public class MaterialSubLotsCommand
    {
        public string MaterialSubLotId { get; set; }
        public double PreviousQuantity { get; set; }
        public double RealQuantity { get; set; }
        public string LocationId { get; set; }
        public string SubLotStatus { get; set; }
        public string UnitOfMeasure { get; set; }

        public MaterialSubLotsCommand(string materialSubLotId, double previousQuantity, double realQuantity, string locationId, string subLotStatus, string unitOfMeasure)
        {
            MaterialSubLotId = materialSubLotId;
            PreviousQuantity = previousQuantity;
            RealQuantity = realQuantity;
            LocationId = locationId;
            SubLotStatus = subLotStatus;
            UnitOfMeasure = unitOfMeasure;
        }
    }
}
