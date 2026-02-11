namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialLots
{
    public class UpdateMaterialLotQuantityCommand : IRequest<bool>
    {
        public string MaterialLotId { get; set; }

        public UpdateMaterialLotQuantityCommand(string materialLotId)
        {
            MaterialLotId = materialLotId;
        }
    }
}
