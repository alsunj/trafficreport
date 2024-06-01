using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
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
        query = query.OrderBy(c => c.CreatedAt);

        return (await query.ToListAsync()).Select(e => Mapper.Map(e));
    }
    public async Task<IEnumerable<DALDTO.Comment>> GetAllViolationCommentsSortedAsync(Guid vehicleViolationId)
    {
        var query = CreateQuery(vehicleViolationId);
        var res = await query.ToListAsync();
        query = query
            .Where(c => c.VehicleViolationId == vehicleViolationId)
            .OrderBy(comment => comment.CreatedAt);
         
        return (await query.ToListAsync()).Select(comment => Mapper.Map(comment));
    }

    public async Task<IEnumerable<DALDTO.Comment>> GetAllViolationCommentsWithNoParentCommentAsync(Guid vehicleViolationId)
    { 
        var query = CreateQuery();
        var res = await query.ToListAsync();
        query = query
            .Where(c => c.VehicleViolationId == vehicleViolationId)
            .Where(c => c.ParentCommentId == null) // Filter comments with empty or null ParentCommentId
            .OrderBy(comment => comment.CreatedAt);
        return (await query.ToListAsync()).Select(comment => Mapper.Map(comment));
        
    }
    public async Task<IEnumerable<DALDTO.Comment>> GetAllViolationCommentsWithParentCommentAsync(Guid parentCommentId)
    { 
        var query = CreateQuery(parentCommentId);
        var res = await query.ToListAsync();
        query = query
            .Where(c => c.ParentCommentId == parentCommentId)
            .OrderBy(comment => comment.CreatedAt);
        return (await query.ToListAsync()).Select(comment => Mapper.Map(comment));
        
    }
}                                                                                                                                                                                  