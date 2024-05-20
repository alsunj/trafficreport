using App.Contracts.DAL.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;

namespace App.DAL.EF.Repositories;

public class VehicleViolationRepository : BaseEntityRepository<APPDomain.Violations.VehicleViolation, DALDTO.VehicleViolation, AppDbContext>,  IVehicleViolationRepository
{

    public VehicleViolationRepository(AppDbContext dbContext, IMapper mapper) :  base(dbContext, new DalDomainMapper<APPDomain.Violations.VehicleViolation, DALDTO.VehicleViolation>(mapper))
    {
    }
    
    public async Task<IEnumerable<DALDTO.VehicleViolation>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        var res = await query.ToListAsync();
        query = query.OrderBy(c => c.Violation);
        
        return (await query.ToListAsync()).Select(e => Mapper.Map(e));
    }
    
    public async Task<IEnumerable<DALDTO.VehicleViolation>> GetAllUserVehicleViolationsSortedAsync(Guid userId)
    {
        // return await CommentRepository
        //     .Where(c => c.VehicleViolationId == vehicleViolationId)
        //     .OrderBy(c => c.CreatedAt)
        //     .ToListAsync();

        var query = CreateQuery(userId);
        var res = await query.ToListAsync();
        query = query
            .Where(v => v.AppUser!.Id == userId)
            //.OrderBy(comment => comment.CreatedAt)
            .OrderBy(v => v.CreatedAt);
        return (await query.ToListAsync()).Select(v => Mapper.Map(v));
    }

    public async Task<IEnumerable<DALDTO.VehicleViolation>> GetAllVehicleViolationsByLicensePlateSortedAsync(string licensePlate)
    {
        var query = CreateQuery();
        query = query
            .Where(v => v.Vehicle!.RegNr!.ToUpper() == licensePlate.ToUpper())
            .OrderBy(v => v.CreatedAt);
        
        return (await query.ToListAsync()).Select(v => Mapper.Map(v));
    }
    
}
