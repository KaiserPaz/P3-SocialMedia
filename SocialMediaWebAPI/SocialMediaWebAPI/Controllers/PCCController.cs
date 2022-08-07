using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaWebAPI.Models.EF;

namespace SocialMediaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCCController : ControllerBase
    {
        private readonly SocialDBContext _context = new SocialDBContext();



        // GET: api/<PCCController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentChildComment>>> GetCComments()
        {
            if (_context.ParentChildComments.Count() == 0)
            {
                return NotFound();
            }
            return await _context.ParentChildComments.ToListAsync();
        }

        // GET api/<PCCController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParentChildComment>> GetCComment(int id)
        {
            if (_context.ParentChildComments.Count() == 0)
            {
                return NotFound();
            }
              var childComment = await _context.ParentChildComments.FindAsync(id);

              if (childComment == null)
              {
                  return NotFound();
              }

              return childComment;
        }

        // POST api/<PCCController>
        [HttpPost]
        public async Task<ActionResult<ParentChildComment>> PostCComment(ParentChildComment Ccomment)
        {
            if (_context.ParentChildComments == null)
            {
                return Problem("Entity set 'SocialDBContext.ParentChildComments'  is null.");
            }
            _context.ParentChildComments.Add(Ccomment);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCComment", new { id = Ccomment.ChildCommentId }, Ccomment);
        }

        // PUT api/<PCCController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCComment(int id, ParentChildComment childComment)
        {
            if (id != childComment.ChildCommentId)
            {
                return BadRequest();
            }

            _context.Entry(childComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CCommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<PCCController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCComment(int id)
        {
            if (_context.ParentChildComments.Count() == 0)
            {
                return NotFound();
            }
            var CComment = await _context.ParentChildComments.FindAsync(id);
            if (CComment == null)
            {
                return NotFound();
            }

            _context.ParentChildComments.Remove(CComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool CCommentExists(int id)
        {
            return (_context.ParentChildComments?.Any(e => e.ChildCommentId == id)).GetValueOrDefault();
        }
    }
}
