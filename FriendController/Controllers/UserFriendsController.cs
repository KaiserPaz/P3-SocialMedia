using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FriendController.Models;

namespace FriendController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFriendsController : ControllerBase
    {
        private readonly SocialDBContext _context;

        public UserFriendsController(SocialDBContext context)
        {
            _context = context;
        }

        private static readonly Expression<Func<User, UserDto>> AsUserDto =
            x => new UserDto
            {
                UserId = x.UserId,
                UserName = x.UserName,
            };

        private static readonly Expression<Func<UserFriend, UserFriendDto>> AsUserFriendDto =
            x => new UserFriendDto
            {
                Id = x.Id,
                UserId = x.UserId,
                FriendId = x.FriendId,
                FriendStatus = x.FriendStatus,
                User = x.User.UserName
            };


        // GET: api/UserFriends
        [HttpGet("ListAll")]
        public async Task<ActionResult<IEnumerable<UserFriendDto>>> GetUserFriends()
        {
            if (_context.UserFriends == null)
            {
                return NotFound();
            }
            return await _context.UserFriends.Include(p => p.User).Select(AsUserFriendDto).ToListAsync();
        }

       
        // GET: api/UserFriends/5
        [HttpGet("FriendsOfUser/")]
        public async Task<ActionResult<UserDto>> GetUserFriend(int userId)
        {
            if (_context.UserFriends == null)
            {
              return NotFound();
            }
            UserDto user = await _context.Users.Include(t => t.UserFriends)
                                             .Where(t => t.UserId == userId)
                                             .Select(AsUserDto)
                                             .FirstOrDefaultAsync();

            if (user == null)
            { 
                return NotFound(); 
            }


            ActionResult<IEnumerable<UserFriendDto>> userFriends = await _context.UserFriends.Include(p => p.User)
                                             .Where(p => p.UserId == user.UserId)
                                             .Select(AsUserFriendDto)
                                             .ToListAsync();

            if (userFriends.Value != null)
            {
                foreach (UserFriendDto userFriend in userFriends.Value)
                {
                    if (user.UserFriends == null)
                        user.UserFriends = new List<UserFriendDto>();
                    user.UserFriends.Add(userFriend);

                }
            }

            return user;
        }

        // PUT: api/UserFriends/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserFriend(int id, UserFriendDto userFriendDto)
        {
            Boolean newFriend = false;

            if (id != userFriendDto.Id)
            {
                return BadRequest("Id does not match");
            }
            try
            {
                //
                // New Friend if this User Friend relationship does not already exist
                //
                var userFriend = await _context.UserFriends.SingleOrDefaultAsync(a => a.Id == id);
                UserFriend userFriend1 = userFriend;
                if (userFriend == null)
                {
                    newFriend = true;
                    userFriend = new UserFriend();
                    userFriend1 = new UserFriend();
                }
                //
                // Fail if the user does not exist
                //
                var user = await _context.Users.SingleOrDefaultAsync(t => t.UserId == userFriendDto.UserId);
                if (user == null)
                {
                    return BadRequest("User does not exists");
                }
                //
                // Fail if the friend does not exist
                //
                user = await _context.Users.SingleOrDefaultAsync(t => t.UserId == userFriendDto.FriendId);
                if (user == null)
                {
                    return BadRequest("Freind does not exists");
                }

                //
                // CLARIFICATION NEEDED!!! is the put based on the ID for UserFriend or the ID for the User? 
                //                         I think that it should be based on the UserId but it reads like the user.
                //
                userFriend.Id = userFriendDto.Id;
                userFriend.UserId = userFriendDto.UserId;
                userFriend.FriendId = userFriendDto.FriendId;
                userFriend.FriendStatus = userFriendDto.FriendStatus;
                //
                // Update date depending on new friend or not
                //
                if (newFriend)
                {
                    userFriend.CreatedAt = DateTime.Now;

                    _context.UserFriends.Add(userFriend);

                    ///Add mirror userFriend

                    userFriend1.Id = userFriendDto.Id + 1000;
                    userFriend1.UserId = userFriendDto.FriendId;
                    userFriend1.FriendId = userFriendDto.UserId;
                    userFriend1.FriendStatus = userFriendDto.FriendStatus;

                    userFriend1.CreatedAt = DateTime.Now;

                    _context.UserFriends.Add(userFriend1);
                } else
                {
                    userFriend.UpdatedAt = DateTime.Now;

                    _context.Entry(userFriend).State = EntityState.Modified;
                }

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
        /*public async Task<IActionResult> PutUserFriend(int id, UserFriend userFriend)
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
        }*/


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
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserFriendExists(userFriend.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

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
