using AutoMapper;
using DataLayer;
using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using WebServer.Model;

namespace WebServer.Controllers
{

    [Route("casino/bets")]
    [ApiController]
    public class BetsController : BaseController
    {

        private readonly IDataserviceBets _dataServiceBets;

        public BetsController(IDataserviceBets dataServiceBets, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataServiceBets = dataServiceBets;
        }


        [HttpGet("all", Name = nameof(GetBets))]
        public IActionResult GetBets(bool includePaging = false, int page = 0, int pageSize = 20)
        {
            if (includePaging)
            {
                var bets = _dataServiceBets.GetBetsWithPaging(page, pageSize);
                if (bets == null)
                {
                    return NotFound();
                }
                var betsModel = CreateBetsModel(bets);

                var total = _dataServiceBets.GetNumberOfBets();

                return Ok(DefaultPagingModel(page, pageSize, total, betsModel, nameof(GetBets)));
            }
            else
            {
                var bets = _dataServiceBets.GetBets();
                if (bets == null)
                {
                    return NotFound();
                }
                var betsModel = CreateBetsModel(bets);
                var total = _dataServiceBets.GetNumberOfBets();

                return Ok(betsModel);
            }
        }

        [HttpGet("Both", Name = nameof(GetBetsFromPlayerAndGame))]
        public IActionResult GetBetsFromPlayerAndGame(GetBetsPlayerGameModel betModel, bool includePaging = false, int page = 0, int pageSize = 20)
        {
            if (includePaging) { 
                var bets = _dataServiceBets.GetBetsFromPlayerAndGameWithPaging(page, pageSize, betModel.PlayerName, betModel.Gid);
                if (bets == null)
                {
                    return NotFound();
                }
                var betsModel = CreateBetsModel(bets);

                var total = _dataServiceBets.GetNumberOfBets();

                return Ok(DefaultPagingModel(page, pageSize, total, betsModel, nameof(GetBetsFromPlayerAndGame)));
            }
            else
            {
                var bets = _dataServiceBets.GetBetsFromPlayerAndGame(betModel.PlayerName, betModel.Gid);
                if (bets == null)
                {
                    return NotFound();
                }
                var betsModel = CreateBetsModel(bets);

                var total = _dataServiceBets.GetNumberOfBets();

                return Ok(betsModel);
            }

        }

        [HttpGet("player/{playername}", Name = nameof(GetBetsFromPlayer))]
        public IActionResult GetBetsFromPlayer(String playername, bool includePaging = false, int page = 0, int pageSize = 20)
        {
            if (includePaging)
            {
                var bets = _dataServiceBets.GetBetsFromPlayerWithPaging(page, pageSize, playername);
                if (bets == null)
                {
                    return NotFound();
                }
                var betsModel = CreateBetsModel(bets);

                var total = _dataServiceBets.GetNumberOfBets();

                return Ok(DefaultPagingModel(page, pageSize, total, betsModel, nameof(GetBetsFromPlayer)));

            }
            else
            {
                var bets = _dataServiceBets.GetBetsFromPlayer(playername);
                if (bets == null)
                {
                    return NotFound();
                }
                var betsModel = CreateBetsModel(bets);

                var total = _dataServiceBets.GetNumberOfBets();

                return Ok(betsModel);
            }
        }

       

        [HttpGet("game/{gid}", Name = nameof(GetBetsFromGame))]
        public IActionResult GetBetsFromGame(int gid, bool includePaging = false, int page = 0, int pageSize = 20)
        {
            if (includePaging)
            {
                var bets = _dataServiceBets.GetBetsFromGameWithPaging(page, pageSize, gid);
                if (bets == null)
                {
                    return NotFound();
                }
                var betsModel = CreateBetsModel(bets);

                var total = _dataServiceBets.GetNumberOfBets();

                return Ok(DefaultPagingModel(page, pageSize, total, betsModel, nameof(GetBetsFromGame)));
            }
            else
            {
                var bets = _dataServiceBets.GetBetsFromGame(gid);
                if (bets == null)
                {
                    return NotFound();
                }
                var betsModel = CreateBetsModel(bets);

                var total = _dataServiceBets.GetNumberOfBets();

                return Ok(betsModel);
            }
               
            
        }

        public IList<BetsModel> CreateBetsModel(IList<BetsDTO> bets)
        {
            var betsModel = new List<BetsModel>();

            foreach (var bet in bets)
            {
                var betsModelElement = _mapper.Map<BetsModel>(bet);
                betsModelElement.Url = _generator.GetUriByName(HttpContext,
                        nameof(BetsController.GetBets),
                        new { id = bet.PlayerName });

                betsModel.Add(betsModelElement);
            }

            return betsModel;
        }
    }
}
