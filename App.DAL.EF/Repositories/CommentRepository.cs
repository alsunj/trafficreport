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
        //query = query.OrderBy(c => c.CreatedAt);
        query = query.OrderBy(c => c.Id);

        return (await query.ToListAsync()).Select(e => Mapper.Map(e));
    }
    public async Task<IEnumerable<DALDTO.Comment>> GetAllViolationCommentsSortedAsync(Guid vehicleViolationId)
    {
        // return await CommentRepository
        //     .Where(c => c.VehicleViolationId == vehicleViolationId)
        //     .OrderBy(c => c.CreatedAt)
        //     .ToListAsync();

        var query = CreateQuery(vehicleViolationId);
        var res = await query.ToListAsync();
        query = query
            .Where(c => c.VehicleViolationId == vehicleViolationId)
            //.OrderBy(comment => comment.CreatedAt)
            .OrderBy(comment => comment.Id);
        return (await query.ToListAsync()).Select(comment => Mapper.Map(comment));




    }
}                                                                                                                                                                                  