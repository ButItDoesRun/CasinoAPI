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
    [Route("api/game")]
    [ApiController]
    public class SpecificGameController : BaseController
    {
        private readonly IDataservice _dataService; 

        public SpecificGameController(IDataservice dataService, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataService = dataService;
        }

        [HttpGet("{id}", Name = nameof(GetGameById))]
        public IActionResult GetGameById(int id)
        {
            var specificGame = _dataService.GetGameById(id);
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

            model.Url = GenerateLink(nameof(GetGameById), new { id = game.Gid });

            return model;
        }


    }
}
