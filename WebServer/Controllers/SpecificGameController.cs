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
    [Route("casino/game")]
    [ApiController]
    public class SpecificGameController : BaseController
    {
        private readonly IDataserviceGame _dataService; 

        public SpecificGameController(IDataserviceGame dataServiceGame, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataService = dataServiceGame;
        }

        [HttpGet("{id}", Name = nameof(GetGameById))]
        public IActionResult GetGameById(int gid)
        {
            var specificGame = _dataService.GetGameById(gid);
            if (specificGame == null)
            {
                return NotFound();
            }
            var specificGameModel = CreateSpecificTitleModel(specificGame);
            return Ok(specificGameModel);
        }

       

        public SpecificGameModel CreateSpecificTitleModel(SpecificGame game)
        {
            var model = _mapper.Map<SpecificGameModel>(game);

            model.Url = GenerateLink(nameof(GetGameById), new { gid = game.Gid });

            return model;
        }


    }
}
