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
                Name = x.Name,
                Address = x.Address,
                Email = x.Email,
                
            }).ToListAsync();
        }

        [HttpGet]
        [Route("api/GetUser/{id}")]
        public async Task<UserDto> GetUser(int id)
        {
            return await db.Users.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
