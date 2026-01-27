namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryIssue
{
    public interface IIssueSubLotRepository : IRepository<IssueSubLot>
    {
        Task<List<IssueSubLot>> GetAllAsync();
        Task<IssueSubLot?> GetByIdAsync(string IssueSubLotId);
    }
}
