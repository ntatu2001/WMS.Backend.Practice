namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialSubLots
{
    public class CreateMaterialSubLotCommand : IRequest<bool>
    {
        public string SubLotId { get; set; }
        public string SubLotStatus { get; set; }
        public double ExistingQuantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public string LocationId { get; set; }
        public string LotNumber { get; set; }

        public CreateMaterialSubLotCommand(string subLotId, string subLotStatus, double existingQuality, string unitOfMeasure, string locationId, string lotNumber)
        {
            SubLotId = subLotId;
            SubLotStatus = subLotStatus;
            ExistingQuantity = existingQuality;
            UnitOfMeasure = unitOfMeasure;
            LocationId = locationId;
            LotNumber = lotNumber;
        }
    }
}
