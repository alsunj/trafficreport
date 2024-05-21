using App.Contracts.DAL.Repositories;
using App.Domain.Vehicles;
namespace App.DAL.EF.Repositories;

public class EVehicleSizeRepository : IEVehicleSizeRepository
{
    public async Task<IEnumerable<string>> GetAllAsync()
    {
        return await Task.Run(() => Enum.GetNames(typeof(EVehicleSize)));
    }
}
