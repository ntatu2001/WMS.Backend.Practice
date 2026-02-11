namespace WMS.Practice.Application.Commands.StockTakeCommands
{
    public class CreateStockTakeCommand : IRequest<string>
    {
        public DateTime? AdjustmentDate { get; set; }
        public string Reason { get; set; }
        public string AdjustmentType { get; set; }
        public string? Note { get; set; }
        public string LotNumber { get; set; }
        public string WarehouseId { get; set; }
        public string EmployeeId { get; set; }

        public CreateStockTakeCommand(DateTime? adjustmentDate, string reason, string adjustmentType, string? note, string lotNumber, string warehouseId, string employeeId)
        {
            AdjustmentDate = adjustmentDate;
            Reason = reason;
            AdjustmentType = adjustmentType;
            Note = note;
            LotNumber = lotNumber;
            WarehouseId = warehouseId;
            EmployeeId = employeeId;
        }
    }
}
