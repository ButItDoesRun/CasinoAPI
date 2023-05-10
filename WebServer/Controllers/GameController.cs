using AutoMapper;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.Model;

namespace WebServer.Controllers
{
    [Route("casino/game")]
    [ApiController]
    public class GameController : BaseController
    {
        private readonly IDataserviceGame _dataserviceGame;

        public GameController(IDataserviceGame dataserviceGame, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataserviceGame = dataserviceGame;
        }


        [HttpGet("get/{gid}", Name = nameof(GetGame))]
        public IActionResult GetGame(int gid, bool includePot = true, bool includeBet = false, bool includePlayers = false)
        {
            try
            {
                var game = _dataserviceGame.GetGameById(gid);
                if (game == null) return NotFound();
                var gameModel = _mapper.Map<GameModel>(game);

                UrlModel updateGame = new()
                {
                    Url = "",
                    JsonBody = new GameDTO()
                };
                gameModel.UpdateGameURL = updateGame;

                if (includePot)
                {
                    Console.WriteLine("Pot will be included");
                }
                if (includeBet)
                {
                    Console.WriteLine("Bet will be included");
                }
                if (includePlayers)
                {
                    Console.WriteLine("Players will be included");
                }

                return Ok(gameModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }

    }
}
