using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;


public class VehicleTypeRepository : BaseEntityRepository<APPDomain.Vehicles.VehicleType, DALDTO.VehicleType, AppDbContext>,  IVehicleTypeRepository
{
    public VehicleTypeRepository(AppDbContext dbContext, IMapper mapper) :  base(dbContext, new DalDomainMapper<APPDomain.Vehicles.VehicleType, DALDTO.VehicleType>(mapper))
    {
    }
    public async Task<IEnumerable<DALDTO.VehicleType>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        var res = await query.ToListAsync();
        query = query.OrderBy(c => c.Size);
        
        return (await query.ToListAsync()).Select(e => Mapper.Map(e));
    }
}