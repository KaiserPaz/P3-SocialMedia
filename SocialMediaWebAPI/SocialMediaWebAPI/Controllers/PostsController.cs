using Microsoft.EntityFrameworkCore;
using SocialMediaWebAPI.Models.EF;
using Microsoft.AspNetCore.Mvc;

namespace SocialMediaWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly SocialDBContext _context = new SocialDBContext();

        //public PostsController(SocialDBContext context)
        //{
        //    _context = context;
        //}

        #region GET: api/Posts - get all posts
        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
          if (_context.Posts == null)
          {
              return NotFound();
          }
            return await _context.Posts.ToListAsync();
        }
        #endregion

        #region GET: api/Posts/1000 - get post by ID (post Id)
        // GET: api/Posts/1000
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
          if (_context.Posts == null)
          {
              return NotFound();
          }
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }
        #endregion

        #region HttpGet (Select) GetPostByUserId
        [HttpGet("userId/{userId}")]
        public IActionResult GetPostByUserId(int userId)
        {
            Post UserPosts = new Post();

            try
            {
                
                if (UserExists(userId))
                {
                    return Ok(UserPosts.GetPostByUserId(userId));
                }
                else
                {
                    throw new Exception("User Not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Check that the UserID Exists
        private bool UserExists(int userId)
        {
            return (_context.Users?.Any(e => e.UserId == userId)).GetValueOrDefault();
        }
        #endregion

        #region PATCH: api/Posts/1000/{updated message goes here} <- this method updates only id, message, and updatedAt - all else remain the same
        //PATCH: api/Posts/1000/{updated message goes here}
        [HttpPatch("{id}/{message}")]
        public async Task<IActionResult> UpdatePost(int id, string message)
        {
            var current = await _context.Posts.FindAsync(id);

            if (current == null)
            {
                return NotFound();
            }
            if (id != current.Id)
            {
                return BadRequest();
            }

            current.Id = id;
            current.Message = message;
            current.UpdatedAt = DateTime.Now;

            _context.Entry(current).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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
        #endregion

        #region POST: api/Posts - creates new post
        // POST: api/Posts
        [HttpPost]
        //updated this method to control what fields are posted in a new post
        public async Task<ActionResult<Post>> AddNewPost(Post newPost)
        {
          if (_context.Posts == null)
          {
              return Problem("No data was entered, please try again.");
          }
            //updated to have new object name
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            //updated to have new object name
            return CreatedAtAction("GetPosts", newPost);
        }
        #endregion

        #region Delete - deletes post
        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Check Post Exists
        private bool PostExists(int id)
        {
            return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion
    }
}