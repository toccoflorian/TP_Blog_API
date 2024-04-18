using BlogAPI.DTO.User;

namespace BlogAPI.IRepository
{
    public interface IAuthRepository
    {
        public void RegisterAsync(RegisterUserDTO registerUserDTO);
    }
}
