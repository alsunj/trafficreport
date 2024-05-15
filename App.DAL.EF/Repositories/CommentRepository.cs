using App.Contracts.DAL.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace App.DAL.EF.Repositories;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;

public class CommentRepository : BaseEntityRepository<APPDomain.Evidences.Comment, DALDTO.Comment, AppDbContext>,  ICommentRepository                               
{                                                                                                                                                                                  
    public CommentRepository(AppDbContext dbContext, IMapper mapper) :  base(dbContext, new DalDomainMapper<APPDomain.Evidences.Comment, DALDTO.Comment>(mapper))       
    {                                                                                                                                                                              
    }                                                                                                                                                                              
    public async Task<IEnumerable<DALDTO.Comment>> GetAllSortedAsync(Guid userId)                                                                                              
    {                                                                                                                                                                              
        var query = CreateQuery(userId);                                                                                                                                           
        var res = await query.ToListAsync();                                                                                                                                       
//        query = query.OrderBy(c => c.Comment);                                                                                                                               
                                                                                                                                                                                   
        return res.Select(e => Mapper.Map(e));                                                                                                                                     
    }                                                                                                                                                                              
}                                                                                                                                                                                  