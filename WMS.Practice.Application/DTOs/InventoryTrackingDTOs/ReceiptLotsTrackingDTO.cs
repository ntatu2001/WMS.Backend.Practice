namespace WMS.Practice.Application.DTOs.InventoryTrackingDTOs
{
    public class ReceiptLotsTrackingDTO
    {
        public string WarehouseName { get; set; }
        public string WarehouseID { get; set; }
        public string PersonName { get; set; }
        public string PersonId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public double? ImportedQuantity { get; set; }
        public string LotNumber { get; set; }
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string UnitOfMeasure { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LotStatus LotStatus { get; set; }

        public List<ReceiptSubLotDTO> ReceiptSubLots { get; set; }

        public ReceiptLotsTrackingDTO(string warehouseName, string warehouseID, string personName, string personId, string supplierName, string supplierId, DateTime receiptDate, double? importedQuantity, string lotNumber, string materialId, string materialName, string unitOfMeasure, LotStatus lotStatus, List<ReceiptSubLotDTO> receiptSubLots)
        {
            WarehouseName = warehouseName;
            WarehouseID = warehouseID;
            PersonName = personName;
            PersonId = personId;
            SupplierName = supplierName;
            SupplierId = supplierId;
            ReceiptDate = receiptDate;
            ImportedQuantity = importedQuantity;
            LotNumber = lotNumber;
            MaterialId = materialId;
            MaterialName = materialName;
            UnitOfMeasure = unitOfMeasure;
            LotStatus = lotStatus;
            ReceiptSubLots = receiptSubLots;
        }
    }
}
