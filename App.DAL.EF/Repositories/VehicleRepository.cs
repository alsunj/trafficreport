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
        query = query.OrderBy(vehicle => vehicle.RegNr);
        return (await query.ToListAsync()).Select(e => Mapper.Map(e));
    }  
    public async Task<DALDTO.Vehicle> GetByLicensePlateAsync(string licensePlate)
    {
        var query = CreateQuery();
        var vehicle = await query
            .Where(v => v.RegNr!.ToUpper() == licensePlate.ToUpper())
            .FirstOrDefaultAsync();
        return Mapper.Map(vehicle)!;
    }
    
}                                                                                                                                                                                  