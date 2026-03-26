using Microsoft.AspNetCore.Mvc;
using YoutubeClone.Application.Interfaces.Services;
using YoutubeClone.Application.Models.Request.Channels;

namespace YoutubeClone.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController(IChannelService channelService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] CreateChannelRequest model)
        {
            var rsp = channelService.Create(model);
            return Ok(rsp);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllChannelRequest model)
        {
            var rsp = channelService.GetAll(model.Limit ?? 0, model.Offset ?? 0);
            return Ok(rsp);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rsp = channelService.GetById(id);
            return Ok(rsp);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var rsp = channelService.Delete(id);
            return Ok(rsp);
        }
    }
}
