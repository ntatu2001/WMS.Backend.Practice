namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetMaterialIdByLotNumberQuery : IRequest<string>
    {
        public string LotNumber { get; set; }

        public GetMaterialIdByLotNumberQuery(string lotNumber)
        {
            LotNumber = lotNumber;
        }
    }
}
