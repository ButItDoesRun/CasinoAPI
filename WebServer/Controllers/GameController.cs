using AutoMapper;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServer.Model;

namespace WebServer.Controllers
{
    [Route("casino/game")]
    [ApiController]
    public class GameController : BaseController
    {
        private readonly IDataserviceGame _dataserviceGame;
        private readonly IDataserviceMoneyPot _dataservicePot;
        private readonly IDataserviceBets _dataserviceBets;

        public GameController(IDataserviceGame dataserviceGame, IDataserviceMoneyPot dataservicePot, IDataserviceBets dataserviceBets, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataserviceGame = dataserviceGame;
            _dataserviceBets = dataserviceBets;
            _dataservicePot = dataservicePot;
        }

        [HttpGet("create", Name = nameof(CreateGame))]
        public IActionResult CreateGame(GameCreateModel createModel, bool includePot = true, bool includeBets = false, bool includePlayers = false)
        {
            try
            {
                if (createModel.Name == null) return BadRequest();
                if (createModel.MinBet == null) return BadRequest();
                if (createModel.MaxBet == null) return BadRequest();

                var game = _dataserviceGame.CreateGame(createModel.Name, (double)createModel.MinBet, (double)createModel.MaxBet);
                if (game == null) return BadRequest();
                var gameRecordModel = ConstructGameRecordModel(game, includePot, includeBets, includePlayers);

                return Ok(gameRecordModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }

        }


        [HttpGet("get/{gid}", Name = nameof(GetGame))]
        public IActionResult GetGame(int gid, bool includePot = true, bool includeBets = false, bool includePlayers = false)
        {
            try
            {

                var game = _dataserviceGame.GetGameById(gid);
                if (game == null) return NotFound();

                var gameRecordModel = ConstructGameRecordModel(game, includePot, includeBets, includePlayers);

                return Ok(gameRecordModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }

        [HttpGet("update/{gid}", Name = nameof(UpdateGame))]
        public IActionResult UpdateGame(int gid, GameUpdateModel updateModel, bool includePot = true, bool includeBets = false, bool includePlayers = false)
        {
            try
            {
                if (updateModel.Name == null) return BadRequest();
                if (updateModel.MinBet == null) return BadRequest();
                if (updateModel.MaxBet == null) return BadRequest();

                var game = _dataserviceGame.UpdateGame(gid, updateModel.Name, (double)updateModel.MinBet, (double)updateModel.MaxBet);
                if (game == null) return NotFound();

                var gameRecordModel = ConstructGameRecordModel(game, includePot, includeBets, includePlayers);

                return Ok(gameRecordModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }

        [HttpGet("delete/{gid}", Name = nameof(DeleteGame))]
        public IActionResult DeleteGame(int gid)
        {
            try
            {
                var deleted = _dataserviceGame.DeleteGame(gid);
                if (deleted == false) return NotFound();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }

        [NonAction]
        private object ConstructGameRecordModel(GameDTO game, bool includePot, bool includeBet, bool includePlayer)
        {
            var gameModel = ConstructGameModel(game);

            PotModel? potModel = null;
            if (includePot)
            {
                var pot = _dataservicePot.GetGamePot(game.Gid);
                potModel = (pot != null) ? ConstructPotModel(pot) : null;
                Console.WriteLine("Pot will be included");
            }
            if (includeBet)
            {
                Console.WriteLine("Bets will be included");
            }
            if (includePlayer)
            {
                Console.WriteLine("Players will be included");
            }

            var gameRecordModel = ConstructGameRecordObject(gameModel, potModel, null, null);

            return gameRecordModel;
        }

        [NonAction]
        object ConstructGameRecordObject(GameModel game, PotModel? pot, BetModel? bet, PlayerModel? player)
        {
            object gameRecord = new
            {
                Game = game,
                Pot = pot,
                Bet = bet,
                Player = player
            };
            return gameRecord;
        }

    }
}
