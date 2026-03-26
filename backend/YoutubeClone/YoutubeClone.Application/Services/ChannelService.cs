using YoutubeClone.Application.Helpers;
using YoutubeClone.Application.Interfaces.Services;
using YoutubeClone.Application.Models.DTOs;
using YoutubeClone.Application.Models.Request.Channels;
using YoutubeClone.Application.Models.Responses;
using YoutubeClone.Shared;
using YoutubeClone.Shared.Helpers;

namespace YoutubeClone.Application.Services
{
    public class ChannelService(Cache<ChannelDto> _cache) : IChannelService
    {
        public GenericResponse<ChannelDto> Create(CreateChannelRequest model)
        {
            var channel = new ChannelDto
            {
                ChannelId = Guid.NewGuid(),
                UserId = model.UserId,
                Handle = model.Handle,
                DisplayName = model.DisplayName,
                Description = model.Description,
                AvatarURL = model.AvatarUrl,
                BannerURL = model.BannerUrl,
                CreatedAt = DateTimeHelper.UtcNow()
            };
            _cache.Add(channel.ChannelId.ToString(), channel);
            return ResponseHelper.Create(channel);
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

        public GenericResponse<List<ChannelDto>> GetAll(int limit, int offset)
        {
            var channels = _cache.Get();
            return ResponseHelper.Create(channels);
        }

        public GenericResponse<ChannelDto?> GetById(Guid id)
        {
            var channel = _cache.Get(id.ToString());
            return ResponseHelper.Create(channel);
        }
    }
}
