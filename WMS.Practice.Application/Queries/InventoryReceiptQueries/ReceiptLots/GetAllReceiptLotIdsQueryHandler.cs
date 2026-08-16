namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.ReceiptLots
{
    public class GetAllReceiptLotIdsQueryHandler : IRequestHandler<GetAllReceiptLotIdsQuery, IEnumerable<string>>
    {
        private readonly IReceiptLotRepository _receiptLotRepository;

        public GetAllReceiptLotIdsQueryHandler(IReceiptLotRepository receiptLotRepository)
        {
            _receiptLotRepository = receiptLotRepository;
        }

        public async Task<IEnumerable<string>> Handle(GetAllReceiptLotIdsQuery request, CancellationToken cancellationToken)
        {
            return await _receiptLotRepository.GetAllReceiptLotIdsAsync();
        }
    }
}
