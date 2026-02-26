namespace WMS.Practice.Application.DTOs.InventoryIssueDTOs
{
    public class InventoryIssueEntryDTO
    {
        public string? InventoryIssueEntryId { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public double? RequestedQuantity { get; set; }
        public string? Note { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialId { get; set; }
        public string? Unit { get; set; }
        public string? InventoryIssueId { get; set; }
        public string? WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public string? PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? LotNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public IssueLotDTO IssueLot { get; set; } = new IssueLotDTO();

        public InventoryIssueEntryDTO()
        {
        }

        public void MapName(string materialName, string warehouseName, string personName, string lotNumber, DateTime issueDate, string unit)
        {
            MaterialName = materialName;
            WarehouseName = warehouseName;
            PersonName = personName;
            LotNumber = lotNumber;
            IssueDate = issueDate;
            Unit = unit;
        }

        public void MapName(string materialName, string warehouseName, string personName)
        {
            MaterialName = materialName;
            WarehouseName = warehouseName;
            PersonName = personName;
        }

    }
}
