using App.Contracts.DAL.Repositories;
using AutoMapper;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;

namespace App.DAL.EF.Repositories;

public class VehicleViolationRepository : BaseEntityRepository<APPDomain.Violations.VehicleViolation, DALDTO.VehicleViolation, AppDbContext>,  IVehicleViolationRepository
{

    public VehicleViolationRepository(AppDbContext dbContext, IMapper mapper) :  base(dbContext, new DalDomainMapper<APPDomain.Violations.VehicleViolation, DALDTO.VehicleViolation>(mapper))
    {
    }
}