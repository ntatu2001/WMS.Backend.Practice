namespace WMS.Practice.Application.Queries.InventoryIssueQueries.IssueLots
{
    public class GetIssueLotByNotDoneQueryHandler : IRequestHandler<GetIssueLotByNotDoneQuery, IEnumerable<IssueLotDTO>>
    {
        private readonly IIssueLotRepository _issueLotRepository;
        private readonly IMapper _mapper;

        public GetIssueLotByNotDoneQueryHandler(IIssueLotRepository issueLotRepository, IMapper mapper)
        {
            _issueLotRepository = issueLotRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IssueLotDTO>> Handle(GetIssueLotByNotDoneQuery request, CancellationToken cancellationToken)
        {
            var issueLots = await _issueLotRepository.GetIssueLotsNotDone()
                         ?? throw new EntityNotFoundException("Issue Lots could not found");

            issueLots = issueLots.Where(s => s.InventoryIssueEntry.InventoryIssue.WarehouseId == request.WarehouseId).ToList();
            return _mapper.Map<IEnumerable<IssueLotDTO>>(issueLots);
        }
    }
}
