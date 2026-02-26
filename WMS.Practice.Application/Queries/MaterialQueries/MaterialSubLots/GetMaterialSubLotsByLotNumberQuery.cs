namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialSubLots
{
    public class GetMaterialSubLotsByLotNumberQuery : IRequest<IEnumerable<MaterialSubLotDTO>>
    {
        public string LotNumber { get; set; }
        public GetMaterialSubLotsByLotNumberQuery(string lotNumber)
        {
            LotNumber = lotNumber;
        }
    }
}
