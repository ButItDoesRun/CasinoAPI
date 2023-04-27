using AutoMapper;
using AutoMapper.Configuration.Annotations;
using DataLayer;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebServer.Model;

namespace WebServer.Controllers
{
    [Route("casino/player")]
    [ApiController]
    public class SpecificPlayerController : BaseController
    {
        private readonly IDataservice _dataService;

        public SpecificPlayerController(IDataservice dataService, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataService = dataService;
        }

        [HttpGet("{name}", Name = nameof(GetPlayerByName))]
        public IActionResult GetPlayerByName(String name)
        {
            var specificPlayer = _dataService.GetPlayerByName(name);
            if (specificPlayer == null)
            {
                return NotFound();
            }
            var specificPlayerModel = CreateSpecificPlayerModel(specificPlayer);
            return Ok(specificPlayerModel);
        }



        public SpecificPlayerModel CreateSpecificPlayerModel(SpecificPlayer player)
        {
            var model = _mapper.Map<SpecificPlayerModel>(player);

            model.Url = GenerateLink(nameof(GetPlayerByName), new { name = player.PlayerName });

            return model;
        }


    }
}
