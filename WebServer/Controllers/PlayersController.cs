using AutoMapper;
using DataLayer;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using WebServer.Model;

namespace WebServer.Controllers
{

    [Route("casino/players")]
    [ApiController]
    public class PlayersController : BaseController
    {
        private readonly IDataservicePlayers _dataServicePlayers;

        public PlayersController(IDataservicePlayers dataServicePlayers, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataServicePlayers = dataServicePlayers;
        }

        [HttpGet("all", Name = nameof(GetPlayers))]
        public IActionResult GetPlayers(int page = 0, int pageSize = 20)
        {
            var players = _dataServicePlayers.GetPlayers(page, pageSize);
            if (players == null)
            {
                return NotFound();
            }
            var gamesModel = CreatePlayersModel(players);

            var total = _dataServicePlayers.GetNumberOfPlayers();

            return Ok(DefaultPagingModel(page, pageSize, total, gamesModel, nameof(GetPlayers)));

        }


        public IList<PlayerListModel> CreatePlayersModel(IList<PlayerListElement> players)
        {
            var playersModel = new List<PlayerListModel>();

            foreach (var player in players)
            {
                var playersModelElement = _mapper.Map<PlayerListModel>(player);
                playersModelElement.Url = _generator.GetUriByName(HttpContext,
                        nameof(PlayersController.GetPlayers),
                        new { id = player.PlayerName });

                playersModel.Add(playersModelElement);
            }

            return playersModel;
        }
    }
}
