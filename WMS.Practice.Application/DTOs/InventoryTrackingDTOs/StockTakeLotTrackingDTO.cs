namespace WMS.Practice.Application.DTOs.InventoryTrackingDTOs
{
    public class StockTakeLotTrackingDTO
    {
        public string WarehouseName { get; set; }
        public string WarehouseID { get; set; }
        public string PersonName { get; set; }
        public string PersonId { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string LotNumber { get; set; }
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string UnitOfMeasure { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdjustmentStatus LotStatus { get; set; }

        public List<StockTakeSubLotDTO> StockTakeSubLotDTOs { get; set; }

        public StockTakeLotTrackingDTO(string warehouseName, string warehouseID, string personName, string personId, DateTime adjustmentDate, string lotNumber, string materialId, string materialName, string unitOfMeasure, AdjustmentStatus lotStatus, List<StockTakeSubLotDTO> stockTakeSublotDTOs)
        {
            WarehouseName = warehouseName;
            WarehouseID = warehouseID;
            PersonName = personName;
            PersonId = personId;
            AdjustmentDate = adjustmentDate;
            LotNumber = lotNumber;
            MaterialId = materialId;
            MaterialName = materialName;
            UnitOfMeasure = unitOfMeasure;
            LotStatus = lotStatus;
            StockTakeSubLotDTOs = stockTakeSublotDTOs;
        }
    }
}
