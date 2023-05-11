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
        private readonly IDataserviceBets _dataserviceBets;

        public GameController(IDataserviceGame dataserviceGame, IDataserviceBets dataserviceBets, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataserviceGame = dataserviceGame;
            _dataserviceBets = dataserviceBets;
        }


        [HttpGet("get/{gid}", Name = nameof(GetGame))]
        public IActionResult GetGame(int gid, bool includePot = true, bool includeBets = false, bool includePlayers = false)
        {
            try
            {
                var game = _dataserviceGame.GetGameById(gid);
                if (game == null) return NotFound();
                var gameModel = _mapper.Map<GameModel>(game);

                gameModel.UpdateGameUrl =
                    GenerateUrlModel(nameof(GameController.GetGame), new { gid = game.Gid }, game);


                if (includePot)
                {
                    Console.WriteLine("Pot will be included");
                }
                if (includeBets)
                {
                    //_dataserviceBets.GetBets();
                    Console.WriteLine("Bets will be included");
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
