using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF;
using AutoMapper;
using Base.BLL;
using Microsoft.AspNetCore.Http.HttpResults;

namespace App.BLL.Services;

public class VehicleService :
    BaseEntityService<App.DAL.DTO.Vehicle, App.BLL.DTO.Vehicle, IVehicleRepository>,
    IVehicleService
{
    public VehicleService(IAppUnitOfWork uoW, IVehicleRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Vehicle, App.BLL.DTO.Vehicle>(mapper))
    {
    }


    public Task<IEnumerable<Vehicle>> GetAllSortedAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Vehicle> GetByLicensePlateAsync(string licensePlate)
    {
        var vehicle = await Repository.GetByLicensePlateAsync(licensePlate);
        return Mapper.Map(vehicle)!;
    }

    public double CalculateVehicleRatingByLicensePlate(string licensePlate, decimal? severity)
    {
        var rating = Repository.CalculateVehicleRatingByLicensePlate(licensePlate, severity);
        return rating;
    }
}