using IPTVDirectoryApiCosmosDB.Core.Entities;
using IPTVDirectoryApiCosmosDB.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPTVDirectoryApiCosmosDB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService _ChannelService;

        public ChannelController(IChannelService famliyService) => _ChannelService = famliyService;

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var Channels = await this._ChannelService.ListAllChannels();
            return Ok(Channels);
        }

        [HttpGet, Route("{ChannelId}")]
        public async Task<IActionResult> Get([FromRoute] string ChannelId)
        {
       
            var Channel = await this._ChannelService.GetChannel(ChannelId);
            return Ok(Channel);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Channel Channel)
        {
            var savedChannel = await this._ChannelService.AddChannel(Channel);

            return Ok(savedChannel);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Channel Channel)
        {
            await this._ChannelService.UpdateChannel(Channel);

            return Ok();
        }

        [HttpDelete, Route("{ChannelId}")]
        public async Task<IActionResult> Delete([FromRoute] string ChannelId)
        {
            await this._ChannelService.DeleteChannel(ChannelId);

            return Ok();
        }
    }
}
