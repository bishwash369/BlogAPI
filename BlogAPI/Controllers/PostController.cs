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
    public class PostController : ControllerBase
    {
        public readonly BlogDbContext db;
        public PostController(BlogDbContext DbContext)
        {
            db = DbContext;
        }


        [HttpGet]
        [Route("api/GetAllPosts")]
        public async Task<List<PostDto>> GetAllPosts()
         {
            return await db.Posts.Select(x => new PostDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                //DateTime = x.DateTime,
                AuthorId = x.AuthorId,
            }).ToListAsync();
         }

        [HttpGet]
        [Route("api/GetPostById/{id}")]
        public async Task<Post> GetPostById(int id)
        {
            return await db.Posts.Where(x => x.Id == id).FirstOrDefaultAsync();
        }


        [HttpPost]
        [Route("api/CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody]PostDto post)
        {
            var createPost = new Post()
            {
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId,
               // DateTime = DateTime.Now
            };
            await db.Posts.AddAsync(createPost);
            await db.SaveChangesAsync();
            return Ok("Post successfully created");
        }

        [HttpPut]
        [Route("api/UpdatePost/{id}")]
        public async Task<IActionResult> UpdatePost([FromBody] PostDto update)
        {
            try
            {
                var updatePost = await db.Posts.Where(x => x.Id == update.Id).FirstOrDefaultAsync();
                updatePost.Title = update.Title;
                updatePost.Content = update.Content;
                updatePost.AuthorId = update.AuthorId;
                //updatePost.DateTime = update.DateTime;
                db.Posts.Update(updatePost);
                await db.SaveChangesAsync();
                return Ok("Post succesfully updated");

            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/DeletePost/{id}")]
        public async Task<IActionResult> DeletePost(int delete)
        {
            try
            {
                var deletePost = await db.Posts.Where(x => x.Id == delete).FirstOrDefaultAsync();
                db.Posts.Remove(deletePost);
                await db.SaveChangesAsync();
                return Ok("Post succesfuly deleted");

            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }  
}
