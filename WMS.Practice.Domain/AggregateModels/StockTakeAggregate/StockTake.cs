namespace WMS.Practice.Domain.AggregateModels.StockTakeAggregate
{
    public class StockTake : Entity, IAggregateModel
    {
        public string StockTakeId { get; private set; }
        public DateTime AdjustmentDate { get; private set; }
        public double PreviousQuantity { get; set; }
        public double AdjustedQuantity { get; set; }
        public AdjustmentReason Reason { get; set; }
        public AdjustmentStatus Status { get; set; }
        public AdjustmentType Type { get; set; }
        public string Note { get; set; }


        public string LotNumber { get; private set; }
        public MaterialLot MaterialLot { get; private set; }

        public string EmployeeId { get; private set; }
        public Employee Employee { get; private set; }

        public string WarehouseId { get; private set; }
        public Warehouse Warehouse { get; private set; }

        public List<StockTakeSubLot> SubLots { get; set; }
        
        public StockTake(string stockTakeId, DateTime adjustmentDate, string lotNumber, string employeeId, string warehouseId)
        {
            StockTakeId = stockTakeId;
            AdjustmentDate = adjustmentDate;
            LotNumber = lotNumber;
            EmployeeId = employeeId;
            WarehouseId = warehouseId;
            SubLots = new List<StockTakeSubLot>();
        }
    }
}
