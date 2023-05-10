using AutoMapper;
using AutoMapper.Configuration.Annotations;
using DataLayer;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebServer.Model;
using WebServer.Services;

namespace WebServer.Controllers
{
    [Route("casino/player")]
    [ApiController]
    public class PlayerController : BaseController
    {
        private readonly IDataservicePlayer _dataServicePlayer;
        private readonly Hashing _hashing;

        public PlayerController(IDataservicePlayer dataServicePlayer, LinkGenerator generator, Hashing hashing, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataServicePlayer = dataServicePlayer;
            _hashing = hashing;
        }


        //COMMANDS FOR A PLAYER


        [HttpPost("post/{playername}/{password}")]
        public IActionResult Register(string playername, string password, string birthdate = "")
        {
            DateOnly birthDate;

            //playername and password cannot be null
            if (playername.IsNullOrEmpty()) return BadRequest();
            if (password.IsNullOrEmpty()) return BadRequest();
            
            //balance is always set to 0 for a new player
            double balance = 0;

            //cheking if the birthdate parameter can be converted to a DateOnly object
            //if false, a standard date is assigned
            if (DateOnly.TryParse(birthdate, out DateOnly result))
            { 
                birthDate = DateOnly.Parse(birthdate);
            }
            else
            {
                birthDate = new DateOnly(1990,01,01);
            }


            //username must be unique
            if (_dataServicePlayer.PlayerExists(playername)) return BadRequest();

            //password must have a minimum length of 8.
            const int minimumPasswordLength = 8;
            if (password.Length < minimumPasswordLength) return BadRequest();

            //password is hashed
            var hashResult = _hashing.Hash(password);

            var created = _dataServicePlayer.CreatePlayer(playername, hashResult.hash,birthDate, balance, hashResult.salt);
            if (!created) return BadRequest();
            return Ok();
        }




        [HttpGet("get/{name}", Name = nameof(GetPlayerByName))]
        public IActionResult GetPlayerByName(String name)
        {
            var specificPlayer = _dataServicePlayer.GetPlayerByName(name);
            if (specificPlayer == null)
            {
                return NotFound();
            }
            var specificPlayerModel = CreatePlayerModel(specificPlayer);
            return Ok(specificPlayerModel);
        }



        public PlayerModel CreatePlayerModel(PlayerDTO player)
        {
            var model = _mapper.Map<PlayerModel>(player);

            model.Url = GenerateLink(nameof(GetPlayerByName), new { name = player.PlayerName });

            return model;
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


    }
}
