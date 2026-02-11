namespace WMS.Practice.Application.Commands.InventoryIssueCommands.IssueSubLots
{
    public class UpdateIssueSubLotsCommandHandler : IRequestHandler<UpdateIssueSubLotsCommand, bool>
    {
        private readonly IIssueLotRepository _issueLotRepository;
        private readonly IIssueSubLotRepository _issueSubLotRepository;

        public UpdateIssueSubLotsCommandHandler(IIssueLotRepository issueLotRepository, IIssueSubLotRepository issueSubLotRepository)
        {
            _issueLotRepository = issueLotRepository;
            _issueSubLotRepository = issueSubLotRepository;
        }

        /// <summary>
        /// API to create Issue SubLots after implementing Issue Lots Storage Assignment Algorithm
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="DuplicateRecordException"></exception>
        public async Task<bool> Handle(UpdateIssueSubLotsCommand request, CancellationToken cancellationToken)
        {
            // Group Issue SubLots based on IssueLotId (or LotNumber)
            var issueSubLotGroups = GroupSubLotsByIssueLotId(request.IssueSubLots);

            foreach (var subLotGroup in issueSubLotGroups)
            {
                var issueLot = await _issueLotRepository.GetIssueLotByIdAsync(subLotGroup.Key)
                            ?? throw new EntityNotFoundException(nameof(ReceiptLot), subLotGroup.Key);

                foreach (var subLot in subLotGroup.Value)
                {
                    if (await _issueSubLotRepository.ExistsAsync(subLot.IssueSublotId) is true)
                        throw new DuplicateRecordException(nameof(IssueSubLot), subLot.IssueSublotId);

                    var issueSubLot = new IssueSubLot(issueSubLotId: subLot.IssueSublotId,
                                                      requestedQuantity: subLot.RequestedQuantity,
                                                      materialSubLotId: subLot.MaterialSubLotId,
                                                      issueLotId: subLot.IssueLotId);

                    issueLot.AddSubLot(issueSubLot);
                }

                issueLot.Update(lotStatus: LotStatus.InProgress);
                issueLot.InventoryIssueEntry.InventoryIssue.Update(issueStatus: IssueStatus.InProgress);
            }

            return await _issueLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        }

        private Dictionary<string, List<IssueSubLotNewDTO>> GroupSubLotsByIssueLotId(IList<IssueSubLotNewDTO> issueSubLots)
        {
            return issueSubLots.GroupBy(x => x.IssueLotId)
                               .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
