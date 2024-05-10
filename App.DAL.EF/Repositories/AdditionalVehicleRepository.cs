using App.Contracts.DAL.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;

public class AdditionalVehicleRepository : BaseEntityRepository<APPDomain.Vehicles.AdditionalVehicle, DALDTO.AdditionalVehicle, AppDbContext>,  IAdditionalVehicleRepository                               
{                                                                                                                                                                                  
    public AdditionalVehicleRepository(AppDbContext dbContext, IMapper mapper) :  base(dbContext, new DalDomainMapper<APPDomain.Vehicles.AdditionalVehicle, DALDTO.AdditionalVehicle>(mapper))       
    {                                                                                                                                                                              
    }                                                                                                                                                                              
    public async Task<IEnumerable<DALDTO.AdditionalVehicle>> GetAllSortedAsync(Guid userId)                                                                                              
    {                                                                                                                                                                              
        var query = CreateQuery(userId);                                                                                                                                           
        var res = await query.ToListAsync();                                                                                                                                       
//        query = query.OrderBy(c => c.AdditionalVehicle);                                                                                                                               
                                                                                                                                                                                   
        return res.Select(e => Mapper.Map(e));                                                                                                                                     
    }                                                                                                                                                                              
}                                                                                                                                                                                  