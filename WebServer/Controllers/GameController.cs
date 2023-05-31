using AutoMapper;
using DataLayer.DatabaseModel.CasinoModel;
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

        public GameController(IDataserviceGame dataserviceGame, IDataserviceMoneyPot dataservicePot, IDataserviceBets dataserviceBets, LinkGenerator generator, IMapper mapper, IConfiguration configuration, IDataservicePlayer dataServicePlayer) : base(generator, mapper, configuration, dataServicePlayer)
        {
            _dataserviceGame = dataserviceGame;
            _dataserviceBets = dataserviceBets;
            _dataservicePot = dataservicePot;
        }

        [HttpGet("create", Name = nameof(CreateGame))]
        public IActionResult CreateGame(GameCreateModel createModel, bool includePot = false, bool includeBets = false)
        {
            try
            {
                if (createModel.Name == null) return BadRequest();
                if (createModel.MinBet == null) return BadRequest();
                if (createModel.MaxBet == null) return BadRequest();

                var game = _dataserviceGame.CreateGame(createModel.Name, (double)createModel.MinBet, (double)createModel.MaxBet);
                if (game == null) return BadRequest();
                var gameRecordModel = ConstructGameRecordModel(game, includePot, includeBets);

                return Ok(gameRecordModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }

        }


        [HttpGet("get/{gid}", Name = nameof(GetGame))]
        public IActionResult GetGame(int gid, bool includePot = false, bool includeBets = false)
        {
            try
            {

                var game = _dataserviceGame.GetGameById(gid);
                if (game == null) return NotFound();

                var gameRecordModel = ConstructGameRecordModel(game, includePot, includeBets);

                return Ok(gameRecordModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }

        [HttpGet("update/{gid}", Name = nameof(UpdateGame))]
        public IActionResult UpdateGame(int gid, GameUpdateModel updateModel, bool includePot = false, bool includeBets = false)
        {
            try
            {
                if (updateModel.Name == null) return BadRequest();
                if (updateModel.MinBet == null) return BadRequest();
                if (updateModel.MaxBet == null) return BadRequest();

                var game = _dataserviceGame.UpdateGame(gid, updateModel.Name, (double)updateModel.MinBet, (double)updateModel.MaxBet);
                if (game == null) return NotFound();

                var gameRecordModel = ConstructGameRecordModel(game, includePot, includeBets);

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
        private object ConstructGameRecordModel(GameDTO game, bool includePot, bool includeBet)
        {            

            var gameModel = ConstructGameModel(game);
            if (includePot || includeBet)
            {
                PotModel? potModel = null;
                if (includePot)
                {
                    var pot = _dataservicePot.GetGamePot(game.Gid);
                    potModel = (pot != null) ? ConstructPotModel(pot) : null;
                    //if the game doesn't have a pot, we provide an option to create one.
                    if (potModel == null)
                    {
                        potModel = new PotModel();
                        var potCreateModel = new PotCreateModel();
                        potModel.CreatePotUrl = GenerateUrlModel(nameof(PotController.CreatePot), new { gid = game.Gid }, potCreateModel);
                    }

                }

                IList<BetModel>? betsModel = null;
                if (includeBet)
                {
                    var bets = _dataserviceBets.GetGameBets(game.Gid);
                    betsModel = (bets != null) ? bets.Select(bet => ConstructBetModel(bet)).ToList() : null;
                    //if the game doesn't have a bet, we provide an option to create one.
                    if (betsModel == null || !betsModel.Any())
                    {
                        BetModel betModel = new BetModel();
                        var betCreateModel = new BetCreateModel();
                        betModel.CreateBetUrl = GenerateUrlModel(nameof(BetController.CreateBet), new {}, betCreateModel);

                        var betModelList = new List<BetModel>();
                        betModelList.Add(betModel);
                        betsModel = betModelList;
                    }
                }

                var gameRecordModel = ConstructGameRecordObject(gameModel, potModel, betsModel);

                return gameRecordModel;
            }
            
            return gameModel;
        }

        [NonAction]
        object ConstructGameRecordObject(GameModel game, PotModel? pot, IList<BetModel>? bets)
        {
            object gameRecord = new
            {
                Game = game,
                Pot = pot,
                Bets = bets,
            };
            return gameRecord;
        }

    }
}
