using BlogAPI.DTO.Comment;
using BlogAPI.IRepository;
using BlogAPI.Models;
using BlogAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        public CommentController(ICommentRepository commentRepository, IUserRepository userRepository)
        {
            this._commentRepository = commentRepository;
            this._userRepository = userRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> Create(CreateCommentDTO createCommentDTO)
        {
            try
            {
                string? appUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value; 
                User? user = await this._userRepository.GetUserByAppUserIdAsync(appUserId);
                createCommentDTO.UserId = user.Id;
                await this._commentRepository.CreateAsync(createCommentDTO);
                return Ok("Commentaire enregistré !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetByArticle(int articleId)
        {
            return Ok(await this._commentRepository.GetByArticleAsync(articleId));
        }
    }
}
