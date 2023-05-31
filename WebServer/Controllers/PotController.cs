using AutoMapper;
using DataLayer;
using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.Model;

namespace WebServer.Controllers
{
    [Route("casino/gamepot")]
    [ApiController]
    public class PotController : BaseController
    {
        private readonly IDataserviceMoneyPot _dataservicePot;

        public PotController(IDataserviceMoneyPot dataservicePot, LinkGenerator generator, IMapper mapper, IConfiguration configuration, IDataservicePlayer dataServicePlayer) : base(generator, mapper, configuration, dataServicePlayer)
        {
            _dataservicePot = dataservicePot;
        }

        [HttpGet("create/{gid}", Name = nameof(CreatePot))]
        public IActionResult CreatePot(int gid, PotCreateModel createModel)
        {
            try
            {
                if (createModel.Amount == null) return BadRequest();

                var pot = _dataservicePot.AddGamePot(gid, (double)createModel.Amount);
                if (pot == null) return BadRequest();

                var potModel = ConstructPotModel(pot);
                return Ok(potModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }

        [HttpGet("get/{gid}", Name = nameof(GetPot))]
        public IActionResult GetPot(int gid)
        {
            var pot = _dataservicePot.GetGamePot(gid);
            if (pot == null) return NotFound();
            var potModel = ConstructPotModel(pot);
            return Ok(potModel);
        }

        [HttpGet("update/{gid}", Name = nameof(UpdatePot))]
        public IActionResult UpdatePot(int gid, PotUpdateModel updateModel)
        {
            try
            {
                if (updateModel.Amount == null) return BadRequest();

                var pot = _dataservicePot.UpdateGamePot(gid, (double)updateModel.Amount);
                if (pot == null) return NotFound();

                var potModel = ConstructPotModel(pot);
                return Ok(potModel);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }


        [HttpGet("delete/{gid}", Name = nameof(DeletePot))]
        public IActionResult DeletePot(int gid)
        {
            try
            {
                var deleted = _dataservicePot.DeleteGamePot(gid);
                if (deleted == false) return NotFound();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }


    }
}
