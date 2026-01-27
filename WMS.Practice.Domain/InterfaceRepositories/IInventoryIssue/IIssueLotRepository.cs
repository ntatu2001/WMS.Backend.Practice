namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryIssue
{
    public interface IIssueLotRepository : IRepository<IssueLot>
    {
        Task<List<IssueLot>> GetAllIssueLotsAsync();
        Task<List<IssueLot>> GetIssueLotsNotDone();
        Task<IssueLot?> GetIssueLotByIdAsync(string id);
        Task<IssueLot?> GetIssueByIssueLotIdAsync(string id);
        Task<IssueLot?> GetByIdAsync(string id);
    }
}
