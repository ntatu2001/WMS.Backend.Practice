namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryIssue
{
    public interface IIssueLotRepository : IRepository<IssueLot>
    {
        Task<bool> ExistsAsync(string id);
        Task<List<IssueLot>> GetAllIssueLotsAsync();
        Task<List<IssueLot>> GetIssueLotsNotDone();
        Task<IssueLot?> GetIssueLotByIdAsync(string id);
        Task<IssueLot?> GetIssueLotWithDetailsByIssueLotIdAsync(string id);
    }
}
