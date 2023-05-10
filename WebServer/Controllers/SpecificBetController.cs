using AutoMapper;
using DataLayer;
using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using WebServer.Model;

namespace WebServer.Controllers 
{
    [Route("casino/bet")]
    [ApiController]

    public class SpecificBetController : BaseController
    {
        private readonly IDataserviceBet _dataServiceBet;

        public SpecificBetController(IDataserviceBet dataServiceBet, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataServiceBet = dataServiceBet;
        }

        [HttpGet("{bid}", Name = nameof(GetBetById))]
        public IActionResult GetBetById(int bid)
        {
            var Bet = _dataServiceBet.GetBetById(bid);
            if (Bet == null)
            {
                return NotFound();
            }
            var specificBetModel = CreateSpecificTitleModel(Bet);
            return Ok(specificBetModel);
        }

        [HttpPost]
        public IActionResult CreateBet([FromBody] Bet newBet)
        {
            var bet = _dataServiceBet.CreateBet(newBet.Bid, newBet.PlayerName, newBet.Gid, newBet.Amount, newBet.Date);
            return Ok();
        }

        [HttpDelete("{bid}")]
        public IActionResult DeleteBet(int bid)
        {
            var deleted = _dataServiceBet.DeleteBet(bid);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }

        public SpecificBetModel CreateSpecificTitleModel(Bet bet)
        {
            var model = _mapper.Map<SpecificBetModel>(bet);

            model.Url = GenerateLink(nameof(GetBetById), new { Bid = bet.Bid });

            return model;
        }
    }
}
