using BlogAPI.DTO.Article;
using BlogAPI.IRepository;
using BlogAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        public ArticleController(IArticleRepository articleRepository, IUserRepository userRepository)
        {
            this._articleRepository = articleRepository;
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Get all articles
        /// </summary>
        /// <returns>List of GetArticleDTO</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetArticleDTO>>> GetAll()
        {
            return Ok(await this._articleRepository.GetAllAsync());
        }

        /// <summary>
        /// Get all article ordered by author in ascendant
        /// </summary>
        /// <returns>List of GetArticleDTO</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetArticleDTO>>> GetAllOrderByAuthorAscendant()
        {
            return await this._articleRepository.GetAllOrderByAuthorAscendantAsync();
        }

        /// <summary>
        /// Get all article ordered by author in descendant
        /// </summary>
        /// <returns>List of GetArticleDTO</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetArticleDTO>>> GetAllOrderByAuthorDescendant()
        {
            return await this._articleRepository.GetAllOrderByAuthorDescendantAsync();
        }

        /// <summary>
        /// Get all article ordered by theme in descendant
        /// </summary>
        /// <returns>List of GetArticleDTO</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetArticleDTO>>> GetAllOrderByThemeAscendant()
        {
            return await this._articleRepository.GetAllOrderByThemeAscendantAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<GetArticleDTO>>> GetAllOrderByThemeDescendant()
        {
            return await this._articleRepository.GetAllOrderByThemeDescendantAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<GetArticleDTO>>> GetByAuthor(int authorId)
        {
            return Ok(await this._articleRepository.GetByAuthorAsync(authorId));
        }

        [HttpGet]
        public async Task<ActionResult<List<GetArticleDTO>>> GetByTheme(int themeId)
        {
            return Ok(await this._articleRepository.GetByThemeAsync(themeId));
        }

        [HttpGet]
        public async Task<ActionResult<GetArticleWithAllInformationsDTO>> GetOneById(int articleId)
        {
            try
            {
            return Ok(await this._articleRepository.GetOneByIdAsync(articleId));
            }
            catch
            {
                return NotFound("Aucun article trouvé!");
            }
        }

        [HttpGet]
        public async Task<ActionResult<GetArticleWithAllInformationsDTO>> GetOneByIdWithInformations(int articleId)
        {
            try
            {
                return Ok(await this._articleRepository.GetOneByIdWithInformationsAsync(articleId));
            }
            catch
            {
                return NotFound("Aucun article trouvé !");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetArticleDTO>>> GetMyArticles()
        {
            try
            {
                User? user = await this._userRepository.GetUserByAppUserIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if(user == null)
                {
                    return BadRequest("Une erreur est survenue lors de la recuperation de l'article");
                }
                else
                {
                    return Ok(await this._articleRepository.GetByMySelfAsync(user.Id));
                }
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> CreateArticles(CreateArticleDTO createArticleDTO)
        {
            string? appUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User? user = await this._userRepository.GetUserByAppUserIdAsync(appUserId);
            createArticleDTO.UserId = user.Id;
            try
            {
                return Ok(await this._articleRepository.CreateAsync(createArticleDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<string>> Update(UpdateArticleDTO updateArticleDTO)
        {
            GetArticleDTO? article = await this._articleRepository.GetOneByIdAsync(updateArticleDTO.Id);
            if (article == null)
            {
                return BadRequest("Aucun article ne correspond !");
            }
            string? appUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User? user = await this._userRepository.GetUserByAppUserIdAsync(appUserId);
            if (await this._articleRepository.GetUserIdOfArticle(article.Id) == user.Id)
            {

                await this._articleRepository.UpdateAsync(updateArticleDTO);
                return Ok("Article modifié !");

            }
            else
            {
                return BadRequest("Vous ne pouvez pas modifier un article que vous n'avez pas rédigé !");
            }
            
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int articleId)
        {
            GetArticleDTO? article = await this._articleRepository.GetOneByIdAsync(articleId);
            if (article == null)
            {
                return BadRequest("Aucun article ne correspond !");
            }
            string? appUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User? user = await this._userRepository.GetUserByAppUserIdAsync(appUserId);
            if (await this._articleRepository.GetUserIdOfArticle(article.Id) == user.Id)
            {

                await this._articleRepository.DeleteAsync(articleId);
                return Ok("Article supprimé !");

            }
            else
            {
                return BadRequest("Vous ne pouvez pas supprimer un article que vous n'avez pas rédigé !");
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpgradePriority(int articleId)
        {
            GetArticleDTO? article = await this._articleRepository.GetOneByIdAsync(articleId);
            if (article == null)
            {
                return BadRequest("Aucun article ne correspond !");
            }
            string? appUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User? user = await this._userRepository.GetUserByAppUserIdAsync(appUserId);
            if (await this._articleRepository.GetUserIdOfArticle(article.Id) == user.Id)
            {
                await this._articleRepository.UpgradePriorityAsync(articleId);
                return Ok("La priorité de l'article est maintenant 'haute' !");
            }
            else
            {
                return BadRequest("Vous ne pouvez pas modifier un article que vous n'avez pas rédigé !");
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> DowngradePriority(int articleId)
        {
            GetArticleDTO? article = await this._articleRepository.GetOneByIdAsync(articleId);
            if (article == null)
            {
                return BadRequest("Aucun article ne correspond !");
            }
            string? appUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User? user = await this._userRepository.GetUserByAppUserIdAsync(appUserId);
            if (await this._articleRepository.GetUserIdOfArticle(article.Id) == user.Id)
            {
                await this._articleRepository.DowngradePriorityAsync(articleId);
                return Ok("La priorité de l'article est maintenant 'normale' !");
            }
            else
            {
                return BadRequest("Vous ne pouvez pas modifier un article que vous n'avez pas rédigé !");
            }
        }
    }
}
