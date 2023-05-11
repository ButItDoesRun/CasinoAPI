using AutoMapper;
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

        [HttpGet("get/{gid}", Name = nameof(GetPot))]
        public IActionResult GetPot(int gid)
        {
            return NotFound();
        }

    }
}
