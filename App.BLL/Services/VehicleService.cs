﻿using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class VehicleService :
    BaseEntityService<App.DAL.DTO.Vehicle, App.BLL.DTO.Vehicle, IVehicleRepository>,
    IVehicleService
{
    public VehicleService(IAppUnitOfWork uoW, IVehicleRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Vehicle, App.BLL.DTO.Vehicle>(mapper))
    {
    }

    public async Task<IEnumerable<Vehicle>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
        
    }
}