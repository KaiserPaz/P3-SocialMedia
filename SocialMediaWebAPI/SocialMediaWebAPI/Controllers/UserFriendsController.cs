using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaWebAPI.Models.EF;

namespace SocialMediaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFriendsController : ControllerBase
    {
        private readonly SocialDBContext _context = new SocialDBContext();

        //public UserFriendsController(SocialDBContext context)
        //{
        //    _context = context;
        //}

        // GET: api/UserFriends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserFriend>>> GetUserFriends()
        {
          if (_context.UserFriends == null)
          {
              return NotFound();
          }
            return await _context.UserFriends.ToListAsync();
        }

        // GET: api/UserFriends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserFriend>> GetUserFriend(int id)
        {
          if (_context.UserFriends == null)
          {
              return NotFound();
          }
            var userFriend = await _context.UserFriends.FindAsync(id);

            if (userFriend == null)
            {
                return NotFound();
            }

            return userFriend;
        }

        // PUT: api/UserFriends/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserFriend(int id, UserFriend userFriend)
        {
            if (id != userFriend.Id)
            {
                return BadRequest();
            }

            _context.Entry(userFriend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserFriendExists(id))
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

        // POST: api/UserFriends
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserFriend>> PostUserFriend(UserFriend userFriend)
        {
          if (_context.UserFriends == null)
          {
              return Problem("Entity set 'SocialDBContext.UserFriends'  is null.");
          }
            _context.UserFriends.Add(userFriend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserFriend", new { id = userFriend.Id }, userFriend);
        }

        // DELETE: api/UserFriends/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserFriend(int id)
        {
            if (_context.UserFriends == null)
            {
                return NotFound();
            }
            var userFriend = await _context.UserFriends.FindAsync(id);
            if (userFriend == null)
            {
                return NotFound();
            }

            _context.UserFriends.Remove(userFriend);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserFriendExists(int id)
        {
            return (_context.UserFriends?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
