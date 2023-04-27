using AutoMapper;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;
using WebServer.Model;

namespace WebServer.Controllers
{
    [Route("casino/games")]
    [ApiController]
    public class GamesController : BaseController
    {
        private readonly IDataserviceGames _dataServiceGames;


        public GamesController(IDataserviceGames dataServiceGames, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataServiceGames = dataServiceGames;
        }


        [HttpGet("all", Name = nameof(GetGames))]
        public IActionResult GetGames(int page = 0, int pageSize = 20)
        {
            var games = _dataServiceGames.GetGames(page, pageSize);
            if (games == null)
            {
                return NotFound();
            }
            var gamesModel = CreateGamesModel(games);

            var total = _dataServiceGames.GetNumberOfGames();

            return Ok(DefaultPagingModel(page, pageSize, total, gamesModel, nameof(GetGames)));

        }


        public IList<GameListModel> CreateGamesModel(IList<GameListElement> games)
        {
            var gamesModel = new List<GameListModel>();

            foreach (var game in games)
            {
                var gameModelElement = _mapper.Map<GameListModel>(game);
                gameModelElement.Url = _generator.GetUriByName(HttpContext,
                        nameof(GamesController.GetGames),
                        new { id = game.Gid });

                gamesModel.Add(gameModelElement);
            }

            return gamesModel;
        }
    }
}
