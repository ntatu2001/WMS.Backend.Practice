namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetStorageInfoByLocationIdQuery : IRequest<LocationStorageInfoDTO>
    {
        public string LocationId { get; set; }

        public GetStorageInfoByLocationIdQuery(string locationId)
        {
            LocationId = locationId;
        }
    }
}
