namespace WMS.Practice.Application.DTOs.InventoryReceiptDTOs
{
    public class InventoryReceiptEntryDTO
    {
        public string? InventoryReceiptEntryId { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialId { get; set; }
        public string? Note { get; set; }
        public string? InventoryReceiptId { get; set; }
        public string? LotNumber { get; set; }
        public string? WarehouseName { get; set; }
        public string? PersonName { get; set; }
        public string? Unit { get; set; }
        public DateTime ReceiptDate { get; set; }
        public ReceiptLotDTO ReceiptLot { get; set; } = new ReceiptLotDTO();

        public InventoryReceiptEntryDTO()
        {
        }

        public void MapName(string materialName, string warehouseName, string personName)
        {
            MaterialName = materialName;
            WarehouseName = warehouseName;
            PersonName = personName;
        }
    }
}
