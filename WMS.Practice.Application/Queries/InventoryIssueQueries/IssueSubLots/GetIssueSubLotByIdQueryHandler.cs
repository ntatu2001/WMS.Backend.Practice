using WMS.Practice.Application.Queries.MaterialQueries.MaterialSubLots;

namespace WMS.Practice.Application.Queries.InventoryIssueQueries.IssueSubLots
{
    public class GetIssueSubLotByIdQueryHandler : IRequestHandler<GetIssueSubLotByIdQuery, IssueSubLotDTO>
    {
        private readonly IIssueSubLotRepository _issueSubLotRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetIssueSubLotByIdQueryHandler(IIssueSubLotRepository issueSubLotRepository, IMediator mediator, IMapper mapper)
        {
            _issueSubLotRepository = issueSubLotRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IssueSubLotDTO> Handle(GetIssueSubLotByIdQuery request, CancellationToken cancellationToken)
        {
            var issueSubLot = await _issueSubLotRepository.GetByIdAsync(request.IssueSublotId)
                           ?? throw new EntityNotFoundException($"Issue SubLot with Id {request.IssueSublotId} could not found");

            var materialSubLot = await _mediator.Send(new GetMaterialSubLotByIdQuery(issueSubLot.IssueSubLotId))
                              ?? throw new EntityNotFoundException($"Could not retrieve Material SubLot with Id {issueSubLot.IssueSubLotId} from {nameof(GetMaterialSubLotByIdQuery)}");

            var issueSubLotDTO = new IssueSubLotDTO(issueSublotId: issueSubLot.IssueSubLotId,
                                                    requestedQuantity: issueSubLot.RequestedQuantity,
                                                    materialSublot: materialSubLot,
                                                    issueLotId: issueSubLot.IssueLotId);

            return issueSubLotDTO;
        }
    }
}
