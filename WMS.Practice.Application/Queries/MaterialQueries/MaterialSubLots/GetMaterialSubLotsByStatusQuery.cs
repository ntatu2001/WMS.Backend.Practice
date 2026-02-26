namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialSubLots
{
    public class GetMaterialSubLotsByStatusQuery : IRequest<IEnumerable<MaterialSubLotDTO>>
    {
        public string Status { get; set; }
        public GetMaterialSubLotsByStatusQuery(string status)
        {
            Status = status;
        }
    }
}
