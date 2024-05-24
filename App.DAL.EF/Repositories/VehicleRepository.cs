using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;

public class VehicleRepository : BaseEntityRepository<APPDomain.Vehicles.Vehicle, DALDTO.Vehicle, AppDbContext>,  IVehicleRepository
{
    private readonly VehicleViolationRepository _violationRepository;
    public VehicleRepository(AppDbContext dbContext, IMapper mapper, VehicleViolationRepository violationRepository) :  base(dbContext, new DalDomainMapper<APPDomain.Vehicles.Vehicle, DALDTO.Vehicle>(mapper))
    {
        _violationRepository = violationRepository;
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
    
    public double CalculateVehicleRatingByLicensePlate(string licensePlate)
    {
        var vehicle = GetByLicensePlateAsync(licensePlate).Result;
        var violations = _violationRepository.GetAllViolationIdsByVehicleId(vehicle.Id).Result;
        var totalRating = 25.0;
      

        foreach (var violation in violations)
        {
            totalRating += 5 * (1 - (double) violation.Severity!);
        }

        var averageRating = totalRating / (violations.Count + 5);

        vehicle.Rating = (decimal) averageRating;
        
        return averageRating;
    }
}                                                                                                                                                                                  