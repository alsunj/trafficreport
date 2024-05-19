using System.Security.Policy;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace App.DAL.EF.Repositories;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;

public class EvidenceRepository : BaseEntityRepository<APPDomain.Evidences.Evidence, DALDTO.Evidence, AppDbContext>,
    IEvidenceRepository
{
    public EvidenceRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext,
        new DalDomainMapper<APPDomain.Evidences.Evidence, DALDTO.Evidence>(mapper))
    {
    }

    public async Task<IEnumerable<DALDTO.Evidence>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        var res = await query.ToListAsync();
        query = query.OrderBy(c => c.CreatedAt);

        return (await query.ToListAsync()).Select(e => Mapper.Map(e));

    }

    public async Task<IEnumerable<DALDTO.Evidence>> GetAllViolationEvidencesSortedAsync(Guid vehicleViolationId)
    {
        var query = CreateQuery(vehicleViolationId);
        var res = await query.ToListAsync();
        query = query
            .Where(e => e.VehicleViolationId == vehicleViolationId)
            .OrderBy(evidence => evidence.CreatedAt);
        return (await query.ToListAsync()).Select(evidence => Mapper.Map(evidence));
    }
}                                                                                                                                                                              