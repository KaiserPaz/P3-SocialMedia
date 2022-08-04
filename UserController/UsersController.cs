using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Models.EF;

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SocialDBContext _context = new SocialDBContext();

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{Email}")]
        public async Task<ActionResult<User>> GetUser(string Email)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            //await _context.Users.FindAsync(Email)
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        
        //Put update UserName
        [HttpPut("{Email}/UpdateUserName")]
        public async Task<IActionResult> UpdateUserName(string Email, string UserName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
            if (user == null)
            {
                return BadRequest();
            }

            user.UserName = UserName;
            
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        //Put update password
        [HttpPut("{Email}/UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(string Email, string Password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
            if (user == null)
            {
                return BadRequest();
            }
            if (Password != null)
            {
                user.Password = Password;
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        // Put Update Names
        [HttpPut("{Email}/UpdateName")]
        public async Task<IActionResult> UpdateName(string Email, string FirstName, string LastName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
            if (user == null)
            {
                return BadRequest();
            }
            if (FirstName != null)
            {
                user.FirstName = FirstName;
            }
            if (LastName != null)
            {
                user.LastName = LastName;
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        // PUT: Update Profile
        [HttpPut("{Email}/UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(string Email, string Profile)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
            if (user == null)
            {
                return BadRequest();
            }

            if (Profile != null)
            {
                user.Profile = Profile;
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

// Put Update Profile Pic
[HttpPut("{Email}/UpdateProfiePicture")]
public async Task<IActionResult> UpdateProfilePicture(string Email, string ProfilePicture)
{
    var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
            if (user == null)
            {
                return BadRequest();
            }

            if (ProfilePicture != null)
            {
            user.ProfilePicture= ProfilePicture;
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

//Put Update Intro
[HttpPut("{Email}/UpdateIntro")]
public async Task<IActionResult> UpdateIntro(string Email, string Intro)
{
    var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
            if (user == null)
            {
                return BadRequest();
            }

            if (Intro != null)
            {
            user.Intro = Intro;
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

// POST: api/Users
[HttpPost("{userEmail}")]
        public async Task<ActionResult<User>> PostUser(string userEmail, string UserName)
        {
            User newUser = new User();
            newUser.Email = userEmail;
            newUser.UserName = UserName;
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string Email)
        {
        if (_context.Users == null)
        {
            return NotFound();
        }
        
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);

            if (user == null)
            {
                return NotFound();
            }
            else
            { 
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
