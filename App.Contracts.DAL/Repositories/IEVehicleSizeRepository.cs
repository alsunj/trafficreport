namespace App.Contracts.DAL.Repositories;

    public interface IEVehicleSizeRepository
    {
        Task<IEnumerable<string>> GetAllAsync();
    }
