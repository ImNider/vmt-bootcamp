using YoutubeClone.Application.Helpers;
using YoutubeClone.Application.Interfaces.Services;
using YoutubeClone.Application.Models.DTOs;
using YoutubeClone.Application.Models.Request.Users;
using YoutubeClone.Application.Models.Responses;
using YoutubeClone.Shared;
using YoutubeClone.Shared.Helpers;

namespace YoutubeClone.Application.Services
{
    public class UserService(Cache<UserDto> _cache) : IUserService
    {
        /*private readonly Cache<UserDto> _cache;

        public UserService(Cache<UserDto> cache)
        {
            _cache = cache;
        }*/

        public GenericResponse<UserDto> Create(CreateUserRequest model)
        {
            var user = new UserDto
            {
                UserId = Guid.NewGuid(),
                UserName = model.UserName,
                DisplayName = model.DisplayName,
                Email = model.Email,
                Birthday = model.Birthday,
                Country = model.Country,
                Password = model.Password,
                CreatedAt = DateTimeHelper.UtcNow(),
                DeletedAt = DateTimeHelper.UtcNow()
            };
            _cache.Add(user.UserId.ToString(), user);
            return ResponseHelper.Create(user);
        }

        public GenericResponse<bool> Delete(Guid id)
        {
            var exist = _cache.Get(id.ToString());
            if (exist is null)
            {
                return ResponseHelper.Create(false);
            }
            _cache.Delete(id.ToString());
            return ResponseHelper.Create(true);
        }

        public GenericResponse<List<UserDto>> GetAll(int limit, int offset)
        {
            var users = _cache.Get();
            return ResponseHelper.Create(users);
        }

        public GenericResponse<UserDto> GetById(Guid id)
        {
            var user = _cache.Get(id.ToString());
            //validar
            return ResponseHelper.Create(user);
        }
    }
}
