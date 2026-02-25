namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetLocationByIdQuery : IRequest<LocationDTO>
    {
        public string LocationId { get; set; }

        public GetLocationByIdQuery(string locationId)
        {
            LocationId = locationId;
        }
    }
}
