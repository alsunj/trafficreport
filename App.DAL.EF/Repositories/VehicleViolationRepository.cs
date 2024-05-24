using App.Contracts.DAL.Repositories;
using App.Domain.Identity;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;

namespace App.DAL.EF.Repositories;

public class VehicleViolationRepository : BaseEntityRepository<APPDomain.Violations.VehicleViolation, DALDTO.VehicleViolation, AppDbContext>,  IVehicleViolationRepository
{
    private readonly ViolationRepository _violationRepository;
    public VehicleViolationRepository(AppDbContext dbContext, IMapper mapper, ViolationRepository violationRepository) :  base(dbContext, new DalDomainMapper<APPDomain.Violations.VehicleViolation, DALDTO.VehicleViolation>(mapper))
    {
        _violationRepository = violationRepository;
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
    
    public async Task<IEnumerable<DALDTO.VehicleViolation>> GetAllVehicleViolationsByVehicleId(Guid vehicleId)
    {
        // return await CommentRepository
        //     .Where(c => c.VehicleViolationId == vehicleViolationId)
        //     .OrderBy(c => c.CreatedAt)
        //     .ToListAsync();

        var query = CreateQuery(vehicleId);
        var res = await query.ToListAsync();
        query = query
            .Where(v => v.VehicleId == vehicleId)
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
    


    // public Task<DALDTO.VehicleViolation> CalculateVehicleRatingById(Guid vehicleId)
    // {
    //     var violations = GetAllVehicleViolationsByVehicleId(vehicleId).Result;
    //     var query = CreateQuery();
    //     query = query
    //         .Select(violations)
    //     var totalRating = 0.0;
    //
    //     foreach (var entry in violations)
    //     {
    //         
    //     }
    // }
    
    public async Task<List<DALDTO.Violation>> GetAllViolationIdsByVehicleId(Guid vehicleId)
    {
        var vehicleViolations = await GetAllVehicleViolationsByVehicleId(vehicleId);
        var violations = new List<DALDTO.Violation>();

        foreach (var vehicleViolation in vehicleViolations)
        {
            violations.Add(await _violationRepository.FirstOrDefaultAsync(vehicleViolation.ViolationId));
        }

        return violations;
    }
    
}
