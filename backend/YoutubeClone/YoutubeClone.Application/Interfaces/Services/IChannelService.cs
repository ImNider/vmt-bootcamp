
using YoutubeClone.Application.Models.DTOs;
using YoutubeClone.Application.Models.Request.Channels;
using YoutubeClone.Application.Models.Responses;

namespace YoutubeClone.Application.Interfaces.Services
{
    public interface IChannelService
    {
        public GenericResponse<ChannelDto> Create(CreateChannelRequest model);
        public GenericResponse<List<ChannelDto>> GetAll(int limit, int offset);
        public GenericResponse<ChannelDto?> GetById(Guid id);
        public GenericResponse<bool> Delete(Guid id);
    }
}
