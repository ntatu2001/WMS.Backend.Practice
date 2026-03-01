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

        public StockTake(string stockTakeId, double previousQuantity, double adjustedQuantity, AdjustmentReason reason, AdjustmentStatus status, AdjustmentType type,
                         DateTime adjustmentDate, string lotNumber, string employeeId, string warehouseId, string note)
        {
            StockTakeId = stockTakeId;
            AdjustmentDate = adjustmentDate;
            LotNumber = lotNumber;
            EmployeeId = employeeId;
            WarehouseId = warehouseId;
            PreviousQuantity = previousQuantity;
            AdjustedQuantity = adjustedQuantity;
            Reason = reason;
            Status = status;
            Type = type;
            Note = note;
            SubLots = new List<StockTakeSubLot>();
        }

        public void AddSubLot(StockTakeSubLot stockTakeSubLot)
        {
            SubLots ??= new List<StockTakeSubLot>();
            SubLots.Add(stockTakeSubLot);
        }

        public double GetAdjustedQuantity() => SubLots?.Count > 0 ? SubLots.Sum(x => x.AdjustedQuantity) : 0.0;

        public void Update(AdjustmentStatus? status = null, double? previousQuantity = null, double? adjustedQuantity = null)
        {
            Status = status ?? Status;
            PreviousQuantity = previousQuantity ?? PreviousQuantity;
            AdjustedQuantity = adjustedQuantity ?? AdjustedQuantity;
        }

        public void Confirm(StockTake stockTake, string lotNumber)
        {

            AddDomainEvent(new InventoryLogAddedDomainEvent(transactionType: TransactionType.Adjustment,
                                                            transactionDate: DateTime.Now.ToVietNamTime(),
                                                            previousQuantity: stockTake.PreviousQuantity,
                                                            changedQuantity: stockTake.AdjustedQuantity - stockTake.PreviousQuantity,
                                                            afterQuantity: stockTake.AdjustedQuantity,
                                                            note: "",
                                                            lotNumber: lotNumber,
                                                            warehouseId: stockTake.WarehouseId));
        }

        public string GetAdjustmentTypeString()
        {
            return Type switch
            {
                AdjustmentType.Periodic => "Kiem ke dinh ky",
                AdjustmentType.Continuous => "Kiem ke thuong xuyen",
                AdjustmentType.Random => "Kiem ke dot xuat",
                AdjustmentType.Cycle => "Kiem ke chu ky",
                _ => "Khac"
            };
        }

        public string GetAdjustmentReasonString()
        {
            return Reason switch
            {
                AdjustmentReason.Damaged => "Hong",
                AdjustmentReason.Expired => "Het han",
                AdjustmentReason.Missing => "Mat",
                AdjustmentReason.Overstock => "Du thua",
                AdjustmentReason.Understock => "Thieu",
                AdjustmentReason.Recount => "Kiem dem lai",
                AdjustmentReason.QualityReassessment => "Danh gia lai chat luong",
                _ => "Khac"
            };
        }

        public string GetAdjustmentStatusString()
        {
            return Status switch
            {
                AdjustmentStatus.Pending => "Cho xac nhan",
                AdjustmentStatus.InProgress => "Dang thuc hien",
                AdjustmentStatus.Done => "Da hoan thanh",
                AdjustmentStatus.Cancelled => "Da huy",
                AdjustmentStatus.Suspended => "Tam ngung",
                AdjustmentStatus.Blocked => "Bi chan",
                _ => "Khac"
            };
        }
    }
}
