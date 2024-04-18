using BlogAPI.Const;
using BlogAPI.DTO.Theme;
using BlogAPI.IRepository;
using BlogAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeRepository _themeRepository;
        public ThemeController(IThemeRepository themeRepository)
        {
            this._themeRepository = themeRepository;
        }

        /// <summary>
        /// Get a list of all themes
        /// </summary>
        /// <returns>List of themes</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetThemeDTO>>> GetAll()
        {
            return Ok(await this._themeRepository.GetAllAsync());
        }

        /// <summary>
        /// Create and save a new theme
        /// </summary>
        /// <param name="createThemeDTO"></param>
        [HttpPost]
        [Authorize(Roles = ROLE.ADMIN)]
        public async Task<ActionResult<string>> Create(CreateThemeDTO createThemeDTO)
        {
            try
            {
                await this._themeRepository.CreateAsync(createThemeDTO);
                return Ok("Theme créé");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Delete a theme
        /// </summary>
        /// <param name="deleteThemeDTO"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = ROLE.ADMIN)]
        public async Task<ActionResult<string>> Delete(DeleteThemeDTO deleteThemeDTO)
        {
            try
            {
            await this._themeRepository.DeleteAsync(deleteThemeDTO);
            return Ok("Theme supprimer");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
