using BlogAPI.Models;
using BlogAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly BlogDbContext db;
        public UserController(BlogDbContext DbContext)
        {
            db = DbContext;
        }

        [HttpGet]
        [Route("api/GetAllUsers")]
        public async Task<List<UserDto>> GetAllUsers()
        {
            return await db.Users.Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Email = x.Email,
                
            }).ToListAsync();
        }
        
        
        [HttpGet]
        [Route("api/GetUserById/{id}")]
        public async Task<User> GetUserById(int id)
        {
            return await db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
  

        [HttpPost]
        [Route("api/CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user)
        {
            try
            {
                var newUser = new User()
                {
                    Name = user.Name,
                    Address = user.Address,
                    Email = user.Email
                };
                await db.Users.AddAsync(newUser);
                await db.SaveChangesAsync();
                return Ok("User successfully created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }   
        }

        [HttpPut]
        [Route("api/UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto update)
        {
            try
            {
                 var updateUser = await db.Users.Where(x => x.Id == update.Id).FirstOrDefaultAsync();
                 updateUser.Name = update.Name;
                 updateUser.Address = update.Address;
                 updateUser.Email = update.Email;
                 db.Users.Update(updateUser);
                 await db.SaveChangesAsync();
                 return Ok("User successfully updated");
                }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/DeleteUser/{Id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            try
            {
                var del = await db.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
                db.Users.Remove(del);
                await db.SaveChangesAsync();
                return Ok("User Successfully Deleted");
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
