namespace WMS.Practice.Application.DTOs.InventoryReceiptDTOs
{
    public class ReceiptLotDTO
    {
        public string? ReceiptLotId { get; set; }
        public double? ImportedQuantity { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LotStatus ReceiptLotStatus { get; set; }
        public string? InventoryReceiptEntryId { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string? StorageLevel { get; set; }
        public string? WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public List<ReceiptSubLotDTO> ReceiptSublots { get; set; } = new List<ReceiptSubLotDTO> { };

        public ReceiptLotDTO()
        {
        }

    }
}
