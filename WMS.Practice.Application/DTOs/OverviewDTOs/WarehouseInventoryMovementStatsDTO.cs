namespace WMS.Practice.Application.DTOs.OverviewDTOs
{
    public class WarehouseInventoryMovementStatsDTO
    {
        public WarehouseByReceiptDTO WarehouseByReceipt { get; set; }
        public WarehouseByIssueDTO WarehouseByIssue { get; set; }

        public WarehouseInventoryMovementStatsDTO(WarehouseByReceiptDTO warehouseByReceipt, WarehouseByIssueDTO warehouseByIssue)
        {
            WarehouseByReceipt = warehouseByReceipt;
            WarehouseByIssue = warehouseByIssue;
        }
    }

    public class WarehouseByReceiptDTO
    {
        public int FinishedProductQuantity { get; set; }
        public int SemiFinishedProductQuantity { get; set; }
        public int RawMaterialQuantity { get; set; }
        public int MaterialQuantity { get; set; }
        public int PackagingQuantity { get; set; }

        public WarehouseByReceiptDTO(int finishedProductQuantity, int semiFinishedProductQuantity, int rawMaterialQuantity, int materialQuantity, int packagingQuantity)
        {
            FinishedProductQuantity = finishedProductQuantity;
            SemiFinishedProductQuantity = semiFinishedProductQuantity;
            RawMaterialQuantity = rawMaterialQuantity;
            MaterialQuantity = materialQuantity;
            PackagingQuantity = packagingQuantity;
        }
    }

    public class WarehouseByIssueDTO
    {
        public int FinishedProductQuantity { get; set; }
        public int SemiFinishedProductQuantity { get; set; }
        public int RawMaterialQuantity { get; set; }
        public int MaterialQuantity { get; set; }
        public int PackagingQuantity { get; set; }

        public WarehouseByIssueDTO(int finishedProductQuantity, int semiFinishedProductQuantity, int rawMaterialQuantity, int materialQuantity, int packagingQuantity)
        {
            FinishedProductQuantity = finishedProductQuantity;
            SemiFinishedProductQuantity = semiFinishedProductQuantity;
            RawMaterialQuantity = rawMaterialQuantity;
            MaterialQuantity = materialQuantity;
            PackagingQuantity = packagingQuantity;
        }
    }

}
