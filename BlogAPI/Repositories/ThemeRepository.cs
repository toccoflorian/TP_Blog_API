using BlogAPI.DTO.Theme;
using BlogAPI.IRepository;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repositories
{
    public class ThemeRepository : IThemeRepository
    {
        private readonly Context _context;
        public ThemeRepository(Context context)
        {
            this._context = context;
        }


        public async Task CreateAsync(CreateThemeDTO createThemeDTO)
        {
            await this._context.Themes.AddAsync(new Theme
            {
                Name = createThemeDTO.Name,
            });
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DeleteThemeDTO deleteThemeDTO)
        {
            this._context.Themes.Remove(await this._context.Themes.FindAsync(deleteThemeDTO.Id));
        }


        public async Task<List<GetThemeDTO>> GetAllAsync()
        {
            List<GetThemeDTO> themesDTO = new List<GetThemeDTO>() { };
            List<Theme> themes = await this._context.Themes.Include(t => t.Articles).ToListAsync();
            foreach(Theme theme in themes)
            {
                themesDTO.Add(new GetThemeDTO
                {
                    Name = theme.Name,
                    NbArticles = theme.Articles.Count()
                });
            }
            return themesDTO;
        }



        public async Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
