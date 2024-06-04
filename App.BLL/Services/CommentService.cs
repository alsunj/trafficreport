using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services
{
    public class CommentService :
        BaseEntityService<App.DAL.DTO.Comment, App.BLL.DTO.Comment, ICommentRepository>,
        ICommentService
    {
        public CommentService(IAppUnitOfWork uoW, ICommentRepository repository, IMapper mapper) : base(uoW,
            repository, new BllDalMapper<App.DAL.DTO.Comment, App.BLL.DTO.Comment>(mapper))
        {
        }

        public async Task<IEnumerable<Comment>> GetAllSortedAsync(Guid userId)
        {
            return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<Comment>> GetAllViolationCommentsSortedAsync(Guid violationId)
        {
            return (await Repository.GetAllViolationCommentsSortedAsync(violationId))
                .Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<Comment>> GetAllViolationCommentsWithParentCommentAsync(Guid parentCommentId)
        {
            return (await Repository.GetAllViolationCommentsWithParentCommentAsync(parentCommentId)).Select(e =>
                Mapper.Map(e));
        }
        
        public async Task<IEnumerable<Comment>> GetAllViolationCommentsWithNoParentCommentAsync(Guid violationId)
        {
            return (await Repository.GetAllViolationCommentsWithNoParentCommentAsync(violationId)).Select(e =>
                Mapper.Map(e));
        }
        
    }
}