namespace WMS.Practice.Application.DTOs.StockTakeDTOs
{
    public class StockTakeLotDTO
    {
        public string StockTakeId { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public double? PreviousQuantity { get; set; }
        public double? AdjustedQuantity { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; }
        public string? AdjustmentType { get; set; }
        public string Note { get; set; }
        public string LotNumber { get; set; }
        public string WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string PersonId { get; set; }
        public string PersonName { get; set; }

        public List<StockTakeSubLotDTO> StockTakeSubLots { get; set; }


        public StockTakeLotDTO(string stockTakeId, DateTime adjustmentDate, double? previousQuantity, double? adjustedQuantity, 
                               string note, string lotNumber, string warehouseId, string warehouseName, string personId, string personName,
                               string adjustmentType, string adjustmentReason, string adjustmentStatus)
        {
            StockTakeId = stockTakeId;
            AdjustmentDate = adjustmentDate;
            PreviousQuantity = previousQuantity;
            AdjustedQuantity = adjustedQuantity;
            Note = note;
            LotNumber = lotNumber;
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
            PersonId = personId;
            PersonName = personName;
            AdjustmentType = adjustmentType;
            Reason = adjustmentReason;
            Status = adjustmentStatus;
            StockTakeSubLots = new List<StockTakeSubLotDTO>();
        }

        public void MapName(string warehouseName, string personName)
        {
            WarehouseName = warehouseName;
            PersonName = personName;
        }
    }
}
