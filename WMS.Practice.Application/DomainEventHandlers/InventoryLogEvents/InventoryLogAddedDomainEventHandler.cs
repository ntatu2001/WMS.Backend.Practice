namespace WMS.Practice.Application.DomainEventHandlers.InventoryLogEvents
{
    public class InventoryLogAddedDomainEventHandler : INotificationHandler<InventoryLogAddedDomainEvent>
    {
        private readonly IInventoryLogRepository _inventoryLogRepository;

        public InventoryLogAddedDomainEventHandler(IInventoryLogRepository inventoryLogRepository)
        {
            _inventoryLogRepository = inventoryLogRepository;
        }

        public async Task Handle(InventoryLogAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            // If Transaction Type is Issue, newChangedQuantity has to minus with ChangedQuantity (product leave the warehouse) in Domain Event.
            var newChangedQuantity = notification.IsIssueTransaction() is true
                                   ? -notification.ChangedQuantity
                                   : notification.ChangedQuantity;

            var newInventoryLog = new InventoryLog(inventoryLogId: notification.InventoryLogId,
                                                   transactionType: notification.TransactionType,
                                                   transactionDate: notification.TransactionDate,
                                                   previousQuantity: notification.PreviousQuantity,
                                                   changedQuantity: newChangedQuantity,
                                                   afterQuantity: notification.AfterQuantity,
                                                   note: notification.Note,
                                                   lotNumber: notification.LotNumber,
                                                   warehouseId: notification.WarehouseId);

            _inventoryLogRepository.Create(newInventoryLog);
            await Task.CompletedTask;

        }
    }
}
