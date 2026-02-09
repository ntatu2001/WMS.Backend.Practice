namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceiptEntries
{
    public class CreateInventoryReceiptEntriesCommand : IRequest<List<string>>
    {
        public string InventoryReceiptId { get; set; }
        public List<CreateReceiptEntryCommand> Entries { get; set; }

        public CreateInventoryReceiptEntriesCommand(string inventoryReceiptId, List<CreateReceiptEntryCommand> entries)
        {
            InventoryReceiptId = inventoryReceiptId;
            Entries = entries;
        }
    }

    public class CreateReceiptEntryCommand
    {
        public string MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public double? ImportedQuantity { get; set; }
        public string? Note { get; set; }
        public string LotNumber { get; set; }
        public string? Unit { get; set; }

        public CreateReceiptEntryCommand(string materialId, string? materialName, string? purchaseOrderNumber, double? importedQuantity, string? note, string lotNumber, string? unit)
        {
            MaterialId = materialId;
            MaterialName = materialName;
            PurchaseOrderNumber = purchaseOrderNumber;
            ImportedQuantity = importedQuantity;
            Note = note;
            LotNumber = lotNumber;
            Unit = unit;
        }
    }
}
