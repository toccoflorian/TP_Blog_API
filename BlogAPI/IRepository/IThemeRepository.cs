using BlogAPI.DTO.Theme;
using BlogAPI.Models;

namespace BlogAPI.IRepository
{
    public interface IThemeRepository
    {
        public Task<List<GetThemeDTO>> GetAllAsync();
        public Task CreateAsync(CreateThemeDTO createThemeDTO);
        public Task UpdateAsync();
        public Task DeleteAsync(DeleteThemeDTO deleteThemeDTO);
    }
}
