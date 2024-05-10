using App.Contracts.DAL.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;

public class VehicleRepository : BaseEntityRepository<APPDomain.Vehicles.Vehicle, DALDTO.Vehicle, AppDbContext>,  IVehicleRepository                               
{                                                                                                                                                                                  
    public VehicleRepository(AppDbContext dbContext, IMapper mapper) :  base(dbContext, new DalDomainMapper<APPDomain.Vehicles.Vehicle, DALDTO.Vehicle>(mapper))       
    {                                                                                                                                                                              
    }                                                                                                                                                                              
    public async Task<IEnumerable<DALDTO.Vehicle>> GetAllSortedAsync(Guid userId)                                                                                              
    {                                                                                                                                                                              
        var query = CreateQuery(userId);                                                                                                                                           
        var res = await query.ToListAsync();                                                                                                                                       
//        query = query.OrderBy(c => c.VehicleType);                                                                                                                               
                                                                                                                                                                                   
        return res.Select(e => Mapper.Map(e));                                                                                                                                     
    }                                                                                                                                                                              
}                                                                                                                                                                                  