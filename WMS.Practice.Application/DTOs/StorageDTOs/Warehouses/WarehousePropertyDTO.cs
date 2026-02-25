namespace WMS.Practice.Application.DTOs.StorageDTOs.Warehouses
{
    public class WarehousePropertyDTO
    {
        public string? PropertyId { get; set; }

        public string? PropertyName { get; set; }
        public string? PropertyValue { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string? WarehouseId { get; set; }

        public WarehousePropertyDTO()
        {
        }

        public WarehousePropertyDTO(string propertyId, string propertyName, string propertyValue, UnitOfMeasure unitOfMeasure, string warehouseId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            WarehouseId = warehouseId;
        }
    }
}
