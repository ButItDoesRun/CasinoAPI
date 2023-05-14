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

    public class BetController : BaseController
    {
        private readonly IDataserviceBet _dataServiceBet;

        public BetController(IDataserviceBet dataServiceBet, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataServiceBet = dataServiceBet;
        }

        [HttpGet("{bid}", Name = nameof(GetBetById))]
        public IActionResult GetBetById(int bid)
        {
            var bet = _dataServiceBet.GetBetById(bid);
            if (bet == null)
            {
                return NotFound();
            }
            var BetDTOModel = CreateBetModel(bet);
            return Ok(BetDTOModel);
        }

        [HttpGet("create", Name = nameof(CreateBet))]
        public IActionResult CreateBet(BetCreateModel newBet)
        {
            var bet = _dataServiceBet.CreateBet(newBet.Bid, newBet.PlayerName, newBet.Gid, newBet.Amount, newBet.Date);
            return Ok();
        }   

        [HttpGet("delete/{bid}")]
        public IActionResult DeleteBet(int bid)
        {
            var deleted = _dataServiceBet.DeleteBet(bid);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("update", Name = nameof(UpdateBet))]
        public IActionResult UpdateBet(BetUpdateModel updateBet)
        {
            var bet = _dataServiceBet.UpdateBet(updateBet.Bid, updateBet.Amount, updateBet.Date);
            return Ok(bet);
        }

        [NonAction]
        public BetModel CreateBetModel(BetDTO bet)
        {
            var model = _mapper.Map<BetModel>(bet);

            model.Url = GenerateLink(nameof(GetBetById), new { Bid = bet.Bid });

            return model;
        }
    }
}
