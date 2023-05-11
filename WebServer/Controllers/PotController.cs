using AutoMapper;
using DataLayer;
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

        public PotController(IDataserviceMoneyPot dataservicePot, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataservicePot = dataservicePot;
        }

        [HttpGet("create/{gid}", Name = nameof(CreatePot))]
        public IActionResult CreatePot(int gid)
        {
            try
            {
                var pot = _dataservicePot.GetGamePot(gid);
                if (pot == null) return NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return NotFound();
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
        public IActionResult UpdatePot(int gid)
        {

            return NotFound();
        }


        [HttpGet("delete/{gid}", Name = nameof(DeletePot))]
        public IActionResult DeletePot(int gid)
        {

            return NotFound();
        }


    }
}
