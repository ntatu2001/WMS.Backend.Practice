namespace WMS.Practice.Application.DTOs.InventoryTrackingDTOs
{
    public class IssueLotsTrackingDTO
    {
        public string WarehouseName { get; set; }
        public string WarehouseID { get; set; }
        public string PersonName { get; set; }
        public string PersonId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public DateTime IssueDate { get; set; }
        public double RequestedQuantity { get; set; }
        public string LotNumber { get; set; }
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string UnitOfMeasure { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LotStatus LotStatus { get; set; }

        public List<IssueSubLotDTO> IssueSubLotDTOs { get; set; }

        public IssueLotsTrackingDTO(string warehouseName, string warehouseID, string personName, string personId, string customerName, string customerId, DateTime issueDate, double requestedQuantity, string lotNumber, string materialId, string materialName, string unitOfMeasure, LotStatus lotStatus, List<IssueSubLotDTO> issueSubLotDTOs)
        {
            WarehouseName = warehouseName;
            WarehouseID = warehouseID;
            PersonName = personName;
            PersonId = personId;
            CustomerName = customerName;
            CustomerId = customerId;
            IssueDate = issueDate;
            RequestedQuantity = requestedQuantity;
            LotNumber = lotNumber;
            MaterialId = materialId;
            MaterialName = materialName;
            UnitOfMeasure = unitOfMeasure;
            LotStatus = lotStatus;
            IssueSubLotDTOs = issueSubLotDTOs;
        }
    }
}
