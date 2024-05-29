using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Evidences;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TrafficReport.Helpers;

namespace TrafficReport.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CommentController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        private readonly PublicDTOBllMapper<App.DTO.v1_0.Comment, App.BLL.DTO.Comment> _mapper; 
        public CommentController(IAppBLL bll, IMapper autoMapper, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Comment, App.BLL.DTO.Comment>(autoMapper);
        }

        /// <summary>
        /// Get all comments for current user.
        /// </summary>
        /// <returns>List of comments.</returns>
        [HttpGet("GetComments")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Comment>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Comment>>> GetUserComments()
        {
            var bllCommentResult = (await _bll.Comments.GetAllSortedAsync(
                Guid.Parse(_userManager.GetUserId(User))
                ))
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(bllCommentResult);
        }
        
        [HttpGet("GetAllCommentsWithParentCommentId/{ParentCommentId}")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Comment>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Comment>>> GetAllCommentsWithParentCommentId(Guid ParentCommentId)
        {
            var bllCommentResult = await _bll.Comments.GetAllViolationCommentsWithParentCommentAsync(ParentCommentId);
            return Ok(bllCommentResult);
        }
        [HttpGet("GetAllVehicleViolationCommentsWithNoParentCommentId/{vehicleViolationId}")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Comment>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Comment>>> GetAllCommentsWithNoParentCommentId(Guid vehicleViolationId)
        {
            var bllCommentResult = (await _bll.Comments.GetAllViolationCommentsWithNoParentCommentAsync(vehicleViolationId))
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(bllCommentResult);
        }

        /// <summary>
        /// Get comments by Vehicle violation id.
        /// </summary>
        /// <param name="vehicleViolationId"></param>
        /// <returns>List of comments.</returns>
        [HttpGet("GetAllCommentsByVehicleViolationId/{vehicleViolationId}")]
        [ProducesResponseType(typeof(App.DTO.v1_0.Comment),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Comment>>> GetAllVehicleViolationComments(Guid vehicleViolationId)
        {
            var comments = await _bll.Comments.GetAllViolationCommentsSortedAsync(vehicleViolationId);

            if (comments.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(comments);
        }
        
        

        /// <summary>
        /// Edit comment.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPut("put/{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutComment(Guid id, App.DTO.v1_0.Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            if (!await _bll.Comments.ExistsAsync(id))
            {
                return NotFound("id doesnt exist");

            }

            if (comment.AccountId != Guid.Parse(_userManager.GetUserId(User)))
            {
                return Unauthorized();
            }

            var res = _mapper.Map(comment);
            _bll.Comments.Update(res);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add a new comment.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(App.DTO.v1_0.Comment),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Comment>> PostComment(App.DTO.v1_0.Comment comment)
        {
            comment.Id = Guid.NewGuid();
            var mappedComment = _mapper.Map(comment);
            _bll.Comments.Add(mappedComment);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetAllVehicleViolationComments", new { id = mappedComment.VehicleViolationId }, comment);
        }

        /// <summary>
        /// Delete a comment.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")] 
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var comment = await _bll.Comments.FirstOrDefaultAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            if (comment.AccountId != Guid.Parse(_userManager.GetUserId(User)))
            {
                return Unauthorized();
            }
            await _bll.Comments.RemoveAsync(id);
            return NoContent();
        }
        
        private bool CommentExists(Guid id)
        {
            return _bll.Comments.Exists(id);
        }
    }
}
