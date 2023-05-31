using AutoMapper;
using AutoMapper.Configuration.Annotations;
using DataLayer;
using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Security.Claims;
using WebServer.Model;
using WebServer.Services;

namespace WebServer.Controllers
{
    [Route("casino/player")]
    [ApiController]
    [Authorize]
    public class PlayerController : BaseController
    {
        private readonly IDataservicePlayer _dataServicePlayer;
        private readonly Hashing _hashing;
        private readonly IDataserviceGame _dataserviceGame;
        private readonly IDataserviceMoneyPot _dataservicePot;
        private readonly IDataserviceBets _dataserviceBets;
        private readonly Authentication _authentication;

        public PlayerController(IDataservicePlayer dataServicePlayer, IDataserviceGame dataserviceGame, IDataserviceMoneyPot dataservicePot, IDataserviceBets dataserviceBets, LinkGenerator generator, Hashing hashing, Authentication authentication, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration, dataServicePlayer)
        {
            _dataServicePlayer = dataServicePlayer;
            _dataserviceGame = dataserviceGame;
            _dataserviceBets = dataserviceBets;
            _dataservicePot = dataservicePot;
            _hashing = hashing;
            _authentication = authentication;
        }     

        //COMMANDS FOR A PLAYER
        [HttpGet("create")]
        [AllowAnonymous]
        public IActionResult RegisterPlayer(PlayerCreateModel player)
        {
            string authMessage;
            DateOnly birthDate;
            string playername = player.PlayerName!;
            string password = player.Password!;
            string birthdate = player.BirthDate!;           

            //playername and password cannot be null
            if (playername.IsNullOrEmpty()) return BadRequest();
            if (password.IsNullOrEmpty()) return BadRequest();           

            //balance is always set to 50 for a new player
            double balance = 50;

            if (!birthdate.IsNullOrEmpty())
            {

                //cheking if the birthdate parameter can be converted to a DateOnly object
                //if false, a standard date is assigned
                if (DateOnly.TryParse(birthdate, out DateOnly result))
                {
                    birthDate = DateOnly.Parse(birthdate);
                }
                else
                {
                    return BadRequest("Incorrect birthdate format");
                }
            }

            //validate playername characters                     
            authMessage = _authentication.ValidatePlayername(player.PlayerName!);            
            if (!authMessage.IsNullOrEmpty()) { return BadRequest(authMessage); }
           
            //playername must be unique
            if (_dataServicePlayer.PlayerExists(playername)) return BadRequest("Playername must be unique");

            //validate password
            authMessage = _authentication.ValidatePassword(player.Password!);
            if (!authMessage.IsNullOrEmpty()) { return BadRequest(authMessage); }        

            //password is hashed
            var hashResult = _hashing.Hash(password);

            var created = _dataServicePlayer.CreatePlayer(playername, hashResult.hash, birthDate, balance, hashResult.salt);
            if (!created) return BadRequest();
            return Ok("Player is succesfully registered!");

        }


        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login(PlayerLoginModel model)
        {            
            var player = _dataServicePlayer.GetPlayerByID(model.PlayerName!);
            var salt = _dataServicePlayer.GetPlayerSalt(player!.PlayerName!);

            if(player == null) return BadRequest();

            if(!_hashing.Verify(model.Password!,player.Password!, salt.SSalt!)) BadRequest();

            string role;

            if (player.IsDeveloper)
            {
                role = "developer";
            }
            else
            {
                role = "player";

            }
            var jwt = GenerateJwtToken(player.PlayerName!, role);

            return Ok(new { player.PlayerName, token = jwt });
        }


        [HttpGet("update/role/{playername}", Name = nameof(MakeDev))]    
        public IActionResult MakeDev(string playername)
        {
            var isDev = _dataServicePlayer.MakeDeveloper(playername);
            if (!isDev) { return BadRequest(); }          
            return Ok();
        }

        //[Authorize(Roles = "player,developer")]

        [HttpGet("get/{playername}", Name = nameof(GetPlayerByID))]        
        public IActionResult GetPlayerByID(String playername, bool includeGame = false, bool includePot = false, bool includeBet = false)
        {      
           try
            {
                var isAuthorized = PlayerAuthorization(playername);
                if (!isAuthorized) { return Unauthorized();}

                var player = _dataServicePlayer.GetPlayerByID(playername);
                if (player == null)
                {
                    return NotFound();
                }

                var specificPlayerModel = ConstructGameRecordModel(player, includeGame, includePot, includeBet);
                return Ok(specificPlayerModel);


            }
            catch
            {
                return Unauthorized();
            }
        }


        [HttpGet("update/balance/{playername}", Name = nameof(UpdatePlayerBalance))]
        public IActionResult UpdatePlayerBalance(string playername, PlayerBalanceUpdateModel updateModel)
        {
            try
            {               
                var isAuthorized = DevAuthorization(playername);               
                if (!isAuthorized) { return Unauthorized(); }

                if (updateModel.Balance == null) return BadRequest();

                var balance = _dataServicePlayer.UpdatePlayerBalance(playername, (double)updateModel.Balance);
                    
                      
                if (balance == null) return NotFound();

                var playerModel = ConstructPlayerModel(balance);
                return Ok(playerModel);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }

        

        [HttpGet("update/{playername}/{amount}", Name = nameof(AddWinOrLossToPlayerBalance))]
        public IActionResult AddWinOrLossToPlayerBalance(string playername, double? amount)
        {
            try
            {
                var isAuthorized = DevAuthorization(playername);
                if (!isAuthorized) { return Unauthorized(); }

                if (amount == null) return BadRequest();

                var balanceIsUpdated = _dataServicePlayer.AddWinOrLossToPlayerBalance(playername, amount);
                if(balanceIsUpdated) return Ok();
                else return BadRequest();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }
        


        [HttpGet("delete/{playername}", Name = nameof(DeletePlayer))]
        public IActionResult DeletePlayer(string playername)
        {
            try
            {
                var isAuthorized = PlayerAuthorization(playername);
                if (!isAuthorized) { return Unauthorized(); }

                var deleted = _dataServicePlayer.DeletePlayer(playername);
                if (deleted == false) return NotFound();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }

        [HttpGet("update/{playername}", Name = nameof(UpdatePlayer))]
        public IActionResult UpdatePlayer(string playername, PlayerUpdateModel updateModel)
        {
            try
            {
                var isAuthorized = PlayerAuthorization(playername);
                if (!isAuthorized) { return Unauthorized(); }

                DateOnly birthDate;
                const int minimumPasswordLength = 8;

                //playername, password and birthdate cannot be null
                if (playername.IsNullOrEmpty()) return BadRequest();
                if (updateModel.Password.IsNullOrEmpty()) return BadRequest();
                if (updateModel.BirthDate.IsNullOrEmpty()) return BadRequest();

                //password must have a minimum length of 8.
                if (updateModel.Password!.Length < minimumPasswordLength) return BadRequest();


                //cheking if the birthdate parameter can be converted to a DateOnly object
                if (DateOnly.TryParse(updateModel.BirthDate, out DateOnly result))
                {
                    birthDate = DateOnly.Parse(updateModel.BirthDate);
                }
                else
                {
                    return BadRequest();
                }


                //password is hashed
                var hashResult = _hashing.Hash(updateModel.Password);

                var updatedPlayer = _dataServicePlayer.UpdatePlayer(playername, hashResult.hash, hashResult.salt, birthDate);

                var playerModel = ConstructPlayerModel(updatedPlayer);
                return Ok(playerModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }



        //COMMANDS FOR MULTIPLE PLAYERS

        [HttpGet("get", Name = nameof(GetPlayers))]
        public IActionResult GetPlayers(int page = 0, int pageSize = 20, bool includeAll = true)
        {

            try
            {               
                var players = _dataServicePlayer.GetPlayers(page, pageSize);
                if (players == null)
                {
                    return NotFound();
                }
                var gamesModel = CreatePlayersModel(players);

                var total = _dataServicePlayer.GetNumberOfPlayers();

                if (includeAll)
                {
                    Console.WriteLine("All Players will be included");
                }

                return Ok(DefaultPagingModel(page, pageSize, total, gamesModel, nameof(GetPlayers)));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }

        }


        public IList<PlayersModel> CreatePlayersModel(IList<PlayersDTO> players)
        {
            var playersModel = new List<PlayersModel>();

            foreach (var player in players)
            {
                var playersModelElement = _mapper.Map<PlayersModel>(player);
                playersModelElement.Url = _generator.GetUriByName(HttpContext,
                        nameof(PlayerController.GetPlayers),
                        new { id = player.PlayerName });

                playersModel.Add(playersModelElement);
            }

            return playersModel;
        }



        //PLAYER RECORD MODEL
                
        [NonAction]
        private object ConstructGameRecordModel(PlayerDTO player, bool includeGame, bool includePot, bool includeBet)
        {
            var playerModel = ConstructPlayerModel(player);
            IList<PlayerGamesModel>? playerGamesModel = null!;
            //IList<PlayerGamesModel> playerGamesModel;

            

            if (includeGame)
            {                
                var playergames = _dataServicePlayer.GetPlayerGames(playerModel.PlayerName!);       
                               
                if (playergames == null) return BadRequest("No games connected to this player");

                playerGamesModel = new List<PlayerGamesModel>();

                foreach (var game in playergames!)
                {
                    var playerGame = _mapper.Map<PlayerGamesModel>(game);                    

                    IList<BetModel>? betsModel = null;

                    if (!includeBet && !includePot)
                    {
                        playerGame.PotAmount = null; //un-includes the pot                                         
                    }

                    if (!includePot)
                    {
                        playerGame.PotAmount = null; //un-includes the pot  

                    }


                    if (includeBet)
                    {
                        var bets = _dataserviceBets.GetPlayerBets(playerModel.PlayerName!, game.Gid);
                        
                        betsModel = (bets != null) ? bets.Select(bet => ConstructBetModel(bet)).ToList() : null;

                        playerGame.Bets = betsModel;                        

                        //if the game doesn't have a bet, we provide an option to create one.
                        if (betsModel == null || !betsModel.Any())
                        {
                            BetModel betModel = new BetModel();
                            var betCreateModel = new BetCreateModel();
                            betModel.CreateBetUrl = GenerateUrlModel(nameof(BetController.CreateBet), new { }, betCreateModel);

                            var betModelList = new List<BetModel>();
                            betModelList.Add(betModel);
                            betsModel = betModelList;

                            playerGame.Bets = betsModel;

                        }
                        
                    }                  


                  try
                    {                     

                        if (playerGame != null) {                            
                            playerGamesModel.Add(playerGame);

                            Console.WriteLine(playerGame.Name);



                        }
                    }catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return Unauthorized();
                    }


                }             


                var playerRecordModel = ConstructPlayerRecordObject(playerModel, playerGamesModel);

                return playerRecordModel;

            }

            return playerModel;

        }
                

        
               

        [NonAction]
        object ConstructPlayerRecordObject(PlayerModel player, IList<PlayerGamesModel>? games)
        {
            object playerRecord = new
            {
                Player = player,
                Games = games,
            };
            return playerRecord;
        }


    }
}
