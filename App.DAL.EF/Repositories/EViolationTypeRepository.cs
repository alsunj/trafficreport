using App.Contracts.DAL.Repositories;
using App.Domain.Violations;

namespace App.DAL.EF.Repositories
{
    public class EViolationTypeRepository : IEViolationTypeRepository
    {
        public async Task<IEnumerable<string>> GetAllAsync()
        {
            return await Task.Run(() => Enum.GetNames(typeof(EViolationType)));
        }
    }
}