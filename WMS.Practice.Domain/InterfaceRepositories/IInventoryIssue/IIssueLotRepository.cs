namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryIssue
{
    public interface IIssueLotRepository : IRepository<IssueLot>
    {
        Task<List<IssueLot>> GetAllIssueLotsAsync();
        Task<List<IssueLot>> GetIssueLotsNotDone();
        Task<IssueLot?> GetIssueLotByIssueLotIdAsync(string id);
        Task<IssueLot?> GetIssueLotWithDetailsByIssueLotIdAsync(string id);
    }
}
