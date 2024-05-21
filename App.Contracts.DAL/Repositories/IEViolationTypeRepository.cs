namespace App.Contracts.DAL.Repositories
{
    public interface IEViolationTypeRepository
    {
        Task<IEnumerable<string>> GetAllAsync();
    }
}