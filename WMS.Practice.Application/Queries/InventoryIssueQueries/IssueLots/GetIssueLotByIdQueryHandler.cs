namespace WMS.Practice.Application.Queries.InventoryIssueQueries.IssueLots
{
    public class GetIssueLotByIdQueryHandler : IRequestHandler<GetIssueLotByIdQuery, IssueLotDTO>
    {
        private readonly IIssueLotRepository _issueLotRepository;
        private readonly IMapper _mapper;

        public GetIssueLotByIdQueryHandler(IIssueLotRepository issueLotRepository, IMapper mapper)
        {
            _issueLotRepository = issueLotRepository;
            _mapper = mapper;
        }

        public async Task<IssueLotDTO> Handle(GetIssueLotByIdQuery request, CancellationToken cancellationToken)
        {
            var issueLot = await _issueLotRepository.GetIssueLotByIdAsync(request.IssueLotId)
                        ?? throw new EntityNotFoundException($"Issue Lot with Id {request.IssueLotId} could not found");

            return _mapper.Map<IssueLotDTO>(issueLot);
        }
    }
}
