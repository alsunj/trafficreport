using App.Contracts.DAL.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace App.DAL.EF.Repositories;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;

public class EvidenceTypeRepository : BaseEntityRepository<APPDomain.Evidences.EvidenceType, DALDTO.EvidenceType, AppDbContext>,  IEvidenceTypeRepository                         
{                                                                                                                                                                                  
    public EvidenceTypeRepository(AppDbContext dbContext, IMapper mapper) :  base(dbContext, new DalDomainMapper<APPDomain.Evidences.EvidenceType, DALDTO.EvidenceType>(mapper))       
    {                                                                                                                                                                              
    }                                                                                                                                                                              
    public async Task<IEnumerable<DALDTO.EvidenceType>> GetAllSortedAsync(Guid userId)                                                                                              
    {                                                                                                                                                                              
        var query = CreateQuery(userId);                                                                                                                                           
        var res = await query.ToListAsync();                                                                                                                                       
//        query = query.OrderBy(c => c.EvidenceType);                                                                                                                               
                                                                                                                                                                                   
        return res.Select(e => Mapper.Map(e));                                                                                                                                     
    }                                                                                                                                                                              
}                                                                                                                                                                                  