using App.Contracts.DAL.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace App.DAL.EF.Repositories;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;

public class EvidenceRepository : BaseEntityRepository<APPDomain.Evidences.Evidence, DALDTO.Evidence, AppDbContext>,  IEvidenceRepository                               
{                                                                                                                                                                                  
    public EvidenceRepository(AppDbContext dbContext, IMapper mapper) :  base(dbContext, new DalDomainMapper<APPDomain.Evidences.Evidence, DALDTO.Evidence>(mapper))       
    {                                                                                                                                                                              
    }                                                                                                                                                                              
    public async Task<IEnumerable<DALDTO.Evidence>> GetAllSortedAsync(Guid userId)                                                                                              
    {                                                                                                                                                                              
        var query = CreateQuery(userId);                                                                                                                                           
        var res = await query.ToListAsync();                                                                                                                                       
//        query = query.OrderBy(c => c.Evidence);                                                                                                                               
                                                                                                                                                                                   
        return res.Select(e => Mapper.Map(e));                                                                                                                                     
    }                                                                                                                                                                              
}                                                                                                                                                                                  