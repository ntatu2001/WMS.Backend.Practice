using WMS.Practice.Application.Queries.MaterialQueries.MaterialSubLots;

namespace WMS.Practice.Application.Queries.InventoryIssueQueries.IssueSubLots
{
    public class GetAllIssueSubLotsQueryHandler : IRequestHandler<GetAllIssueSubLotsQuery, IEnumerable<IssueSubLotDTO>>
    {
        private readonly IIssueSubLotRepository _issueSubLotRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAllIssueSubLotsQueryHandler(IIssueSubLotRepository issueSubLotRepository, IMediator mediator, IMapper mapper)
        {
            _issueSubLotRepository = issueSubLotRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IssueSubLotDTO>> Handle(GetAllIssueSubLotsQuery request, CancellationToken cancellationToken)
        {
            var issueSubLots = await _issueSubLotRepository.GetAllInventorySubLotsAsync()
                            ?? throw new EntityNotFoundException("Inventory SubLots could not found");

            var issueSubLotsDTOs = new List<IssueSubLotDTO>();
            foreach (var issueSubLot in issueSubLots)
            {
                var materialSubLot = await _mediator.Send(new GetMaterialSubLotByIdQuery(issueSubLot.IssueSubLotId))
                                  ?? throw new EntityNotFoundException($"Could not retrieve Material SubLot with Id {issueSubLot.IssueSubLotId} from {nameof(GetMaterialSubLotByIdQuery)}");

                var issueSubLotDTO = new IssueSubLotDTO(issueSublotId: issueSubLot.IssueSubLotId,
                                                        requestedQuantity: issueSubLot.RequestedQuantity,
                                                        materialSublot: materialSubLot,
                                                        issueLotId: issueSubLot.IssueLotId);

                issueSubLotsDTOs.Add(issueSubLotDTO);
            }

            return issueSubLotsDTOs;
        }
    }
}
