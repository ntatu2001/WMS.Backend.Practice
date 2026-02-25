namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetLocationPropertyByIdQuery : IRequest<LocationPropertyDTO>
    {
        public string LocationPropertyId { get; set; }

        public GetLocationPropertyByIdQuery(string locationPropertyId)
        {
            LocationPropertyId = locationPropertyId;
        }
    }
}
