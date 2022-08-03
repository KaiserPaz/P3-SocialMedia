using Microsoft.AspNetCore.Mvc;
using SocialMediaWebAPI.Models.EF;
using System.Collections;
using System.Linq;

namespace SocialMediaWebAPI.Controllers
{
    /*
     * User controller
     * Get user by ID
     * Get user's posts
     * Get user's friends
     * Retrieve all users
     * Add user
     * Delete user
     * Modify user
     * Add friend to user
     * Add post to user
     * Delete post by user
     * Delete user's friend
     * Edit post by user
     */
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SocialDBContext _context = new SocialDBContext();
        [HttpGet]
        [Route("username/{p_userId}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpGet]
        [Route("userPosts/{p_Id}")]
        public Task<ActionResult<IEnumerable<Post>>> GetAllPostsByUser(int p_Id)
        {
        }
        [HttpPut]
        [Route("playerlist/add/{p_playerName}/{p_playerPosition}/{p_playerTeamId}")]
        public IActionResult AddPlayer(string p_playerName, string p_playerPosition, int p_playerTeamId)
        {
            var playerObj = new Player();
            var player = new Player()
            {
                playerName = p_playerName,
                playerPosition = p_playerPosition,
                playerTeamId = p_playerTeamId
            };
            try
            {
                return Ok(playerObj.AddNewPlayer(player));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("playerlist/update/{p_playerName}/{p_playerTeamId}")]
        public IActionResult UpdatePlayer(string p_playerName, int p_playerTeamId)  //changes player's team
        {
            var playerObj = new Player();
            try
            {
                return Ok(playerObj.UpdatePlayer(p_playerName, p_playerTeamId));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("playerlist/delete/{p_playerName}")]
        public IActionResult DeletePlayer(string p_playerName)
        {
            var playerObj = new Player();
            try
            {
                return Ok(playerObj.DeletePlayer(p_playerName));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
