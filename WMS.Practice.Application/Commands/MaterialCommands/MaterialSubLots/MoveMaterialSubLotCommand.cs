namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialSubLots
{
    public class MoveMaterialSubLotCommand : IRequest<bool>
    {
        public string MaterialSubLotId { get; set; }
        public string ToLocationId { get; set; }

        public MoveMaterialSubLotCommand(string materialSubLotId, string toLocationId)
        {
            MaterialSubLotId = materialSubLotId;
            ToLocationId = toLocationId;
        }
    }
}
