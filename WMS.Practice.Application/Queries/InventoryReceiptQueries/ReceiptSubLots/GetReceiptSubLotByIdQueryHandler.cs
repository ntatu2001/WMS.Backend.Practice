namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.ReceiptSubLots
{
    public class GetReceiptSubLotByIdQueryHandler : IRequestHandler<GetReceiptSubLotByIdQuery, ReceiptSubLotDTO>
    {
        private readonly IReceiptSubLotRepository _receiptSubLotRepository;
        private readonly IMapper _mapper;

        public GetReceiptSubLotByIdQueryHandler(IReceiptSubLotRepository receiptSubLotRepository, IMapper mapper)
        {
            _receiptSubLotRepository = receiptSubLotRepository;
            _mapper = mapper;
        }

        public async Task<ReceiptSubLotDTO> Handle(GetReceiptSubLotByIdQuery request, CancellationToken cancellationToken)
        {
            var receiptSubLot = await _receiptSubLotRepository.GetReceiptSubLotByIdAsync(request.ReceiptSublotId)
                             ?? throw new EntityNotFoundException(nameof(ReceiptSubLot), request.ReceiptSublotId);

            return _mapper.Map<ReceiptSubLotDTO>(receiptSubLot);
        }
    }
}
