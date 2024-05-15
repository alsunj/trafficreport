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
using Asp.Versioning;
using AutoMapper;
using TrafficReport.Helpers;

namespace TrafficReports.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/evidences/[controller]/[action]")]
    public class CommentController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Comment, App.BLL.DTO.Comment> _mapper; 
        public CommentController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Comment, App.BLL.DTO.Comment>(autoMapper);
        }

        // GET: api/Comment
        [HttpGet]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Comment>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Comment>>> GetComments()
        {
            var bllCommentResult = await _bll.Comments.GetAllAsync();
            var bllComments = bllCommentResult.Select(e => _mapper.Map(e)).ToList();
            return Ok(bllComments);
        }

        // GET: api/Comment/5
        [HttpGet]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Comment>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Comment>> GetComment(Guid id)
        {
            var comment = await _bll.Comments.FirstOrDefaultAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(comment);
            
            return Ok(res);
        }

        // PUT: api/Comment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
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
            var res = _mapper.Map(comment);
            _bll.Comments.Update(res);

            return NoContent();
        }

        // POST: api/Comment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(App.DTO.v1_0.Comment),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Comment>> PostComment(App.DTO.v1_0.Comment comment)
        {
            var mappedComments = _mapper.Map(comment);
            _bll.Comments.Add(mappedComments);

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comment/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
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
            await _bll.Comments.RemoveAsync(id);
            return NoContent();
        }

        private bool CommentExists(Guid id)
        {
            return _bll.Comments.Exists(id);
        }
    }
}
