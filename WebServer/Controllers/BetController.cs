using AutoMapper;
using DataLayer;
using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using WebServer.Model;

namespace WebServer.Controllers 
{
    [Route("casino/bet")]
    [ApiController]

    public class BetController : BaseController
    {
        private readonly IDataserviceBet _dataServiceBet;

        public BetController(IDataserviceBet dataServiceBet, LinkGenerator generator, IMapper mapper, IConfiguration configuration, IDataservicePlayer dataServicePlayer) : base(generator, mapper, configuration, dataServicePlayer)
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
            var getBetModel = ConstructBetModel(bet);
            return Ok(getBetModel);
        }

        [HttpGet("create/{gid}", Name = nameof(CreateBet))]
        public IActionResult CreateBet(BetCreateModel createModel, int gid)
        {            
            var bet = _dataServiceBet.CreateBet(createModel.PlayerName!, gid, createModel.Amount);
           

            if (bet == null) return BadRequest();
            var createBetModel = ConstructBetModel(bet);
            return Ok(createBetModel);
        }   

        [HttpGet("delete/{bid}", Name = nameof(DeleteBet))]
        public IActionResult DeleteBet(int bid)
        {
            var deleted = _dataServiceBet.DeleteBet(bid);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok(deleted);
        }

        [HttpGet("update/{bid}", Name = nameof(UpdateBet))]
        public IActionResult UpdateBet(BetUpdateModel updateBet, int bid)
        {
            var bet = _dataServiceBet.UpdateBet(bid, updateBet.Amount);
            if (bet == null) return BadRequest();
            var updateBetModel = ConstructBetModel(bet);
            return Ok(updateBetModel);
        }

    }
}
